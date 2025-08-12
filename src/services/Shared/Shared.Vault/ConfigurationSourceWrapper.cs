using Microsoft.Extensions.Configuration;

namespace Shared.Vault;

public class ConfigurationSourceWrapper : IConfigurationSource
{
    private readonly ConfigurationProvider _provider;
    public ConfigurationSourceWrapper(ConfigurationProvider provider) => _provider = provider;
    public IConfigurationProvider Build(IConfigurationBuilder builder) => _provider;
}