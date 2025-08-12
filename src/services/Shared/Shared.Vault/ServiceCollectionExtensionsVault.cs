using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace Shared.Vault;

public static class ServiceCollectionExtensionsVault
{
    public static IServiceCollection AddVaultConfiguration(
        this IServiceCollection services,
        IConfiguration configuration,
        string path,
        string fieldName)
    {
        // VaultOptions'ı bind et
        services.Configure<VaultOptions>(opts => configuration.GetSection("VaultConfig").Bind(opts));

        // IVaultClient
        services.AddSingleton<IVaultClient>(sp =>
        {
            var opts = sp.GetRequiredService<IOptions<VaultOptions>>().Value;

            var addr = opts.Address ?? Environment.GetEnvironmentVariable("VAULT_ADDR");
            var token = opts.Token ?? Environment.GetEnvironmentVariable("VAULT_TOKEN");

            var auth = new TokenAuthMethodInfo(token);
            return new VaultClient(new VaultClientSettings(addr, auth));
        });

        // Secret service
        services.AddSingleton<IVaultSecretService, VaultSecretService>();

        // Provider'ı configuration pipeline'a ekle (ConfigurationManager bekler)
        if (configuration is IConfigurationBuilder cfgBuilder)
        {
            using var sp = services.BuildServiceProvider();
            var opts = sp.GetRequiredService<IOptions<VaultOptions>>().Value;
            var vaultSvc = sp.GetRequiredService<IVaultSecretService>();

            if (opts.UseCubbyhole)
            {
                cfgBuilder.Add(new ConfigurationSourceWrapper(
                    new VaultConfigurationProvider(vaultSvc, path, fieldName)));
            }
        }

        return services;
    }
}