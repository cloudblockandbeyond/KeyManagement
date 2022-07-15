namespace EmployeeManagement.Api.Models;

public class SigningKey
{
    public PublicKey PublicKey { get; set; } = null!;
    public PrivateKey PrivateKey { get; set; } = null!;
}