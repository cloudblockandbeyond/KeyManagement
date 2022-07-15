using KeyManagement.Api.Models;

namespace KeyManagement.Api.Services;

public interface IKeyManagementService
{
    Task<SigningKey> CreateKeyAsync();
    Task<SigningKey> GetKeyAsync(string kid);
}