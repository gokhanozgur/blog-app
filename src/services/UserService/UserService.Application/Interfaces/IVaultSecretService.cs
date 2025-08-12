namespace UserService.Application.Interfaces;

public interface IVaultSecretService
{
    Task<string> GetCubbyholeFieldAsync(string path, string fieldName);
    string GetCubbyholeField(string path, string fieldName);
}