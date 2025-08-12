namespace Shared.Vault;

public class VaultOptions
{
    public string Address { get; set; } = "http://vault:8200";
    public string Token { get; set; } = "myroot";
    public int EngineVersion { get; set; } = 2;
    public string? MountPoint { get; set; }
    public bool UseCubbyhole { get; set; } = true;
}