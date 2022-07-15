using System.Security.Cryptography;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Cryptography;

public class CryptoProvider
{
    private RSAParameters _publicKey;
    private RSAParameters _privateKey;

    public CryptoProvider()
    {
        using var rsa = new RSACryptoServiceProvider(2048);
        _privateKey = rsa.ExportParameters(true);
        _publicKey = rsa.ExportParameters(false);
    }

    public PublicKey GetPublicKey(string kid)
    {
        var publicKey = new PublicKey
        (
            alg: "RS256",
            kty: "RSA",
            use: "signer",
            n: Convert.ToBase64String(_publicKey.Modulus),
            e: Convert.ToBase64String(_publicKey.Exponent),
            kid: kid,
            iat: DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            exp: DateTimeOffset.UtcNow.AddDays(365).ToUnixTimeSeconds()
        );

        return publicKey;
    }

    public PrivateKey GetPrivateKey(string kid)
    {
        var privateKey = new PrivateKey
        (
            kty: "RSA",
            n: Convert.ToBase64String(_privateKey.Modulus),
            e: Convert.ToBase64String(_privateKey.Exponent),
            d: Convert.ToBase64String(_privateKey.D),
            p: Convert.ToBase64String(_privateKey.P),
            q: Convert.ToBase64String(_privateKey.Q),
            dp: Convert.ToBase64String(_privateKey.DP),
            dq: Convert.ToBase64String(_privateKey.DQ),
            qi: Convert.ToBase64String(_privateKey.InverseQ),
            alg: "RS256",
            use: "signer",
            kid: kid,
            iat: DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            exp: DateTimeOffset.UtcNow.AddDays(365).ToUnixTimeSeconds()
        );

        return privateKey;
    }
}