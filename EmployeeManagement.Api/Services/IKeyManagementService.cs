using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Services;

public interface IKeyManagementService
{
    Task<SigningKey> CreateKeyAsync(CreateKey createKey);
    Task<SigningKey> GetKeyAsync(string kid);
}