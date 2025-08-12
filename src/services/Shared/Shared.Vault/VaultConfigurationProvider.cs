using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Shared.Vault;

public class VaultConfigurationProvider : ConfigurationProvider
{
    private readonly IVaultSecretService _vault;
    private readonly string _path;
    private readonly string _fieldName;

    public VaultConfigurationProvider(IVaultSecretService vault, string path, string fieldName)
    {
        _vault = vault;
        _path = path;
        _fieldName = fieldName;
    }

    public override void Load()
    {
        // Not: Load senkron; burada sync okuyoruz.
        var json = _vault.GetCubbyholeField(_path, _fieldName);

        using var doc = JsonDocument.Parse(json);
        var flat = new Dictionary<string, string?>();
        Flatten("", doc.RootElement, flat);
        Data = flat;
    }

    private static void Flatten(string parent, JsonElement el, IDictionary<string, string?> dict)
    {
        switch (el.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var p in el.EnumerateObject())
                {
                    var key = string.IsNullOrEmpty(parent) ? p.Name : $"{parent}:{p.Name}";
                    Flatten(key, p.Value, dict);
                }
                break;
            case JsonValueKind.Array:
                int i = 0;
                foreach (var item in el.EnumerateArray())
                {
                    Flatten($"{parent}:{i}", item, dict);
                    i++;
                }
                break;
            case JsonValueKind.String:
                dict[parent] = el.GetString();
                break;
            case JsonValueKind.Number:
                dict[parent] = el.GetRawText();
                break;
            case JsonValueKind.True:
            case JsonValueKind.False:
                dict[parent] = el.GetBoolean().ToString();
                break;
            case JsonValueKind.Null:
                dict[parent] = null;
                break;
        }
    }
}