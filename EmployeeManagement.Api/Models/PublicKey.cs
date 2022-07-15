namespace EmployeeManagement.Api.Models;

public record PublicKey
(
    string alg,
    string kty,
    string use,
    string n,
    string e,
    string kid,
    long iat,
    long exp
);