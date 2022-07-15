using KeyManagement.Api.Cryptography;
using KeyManagement.Api.Models;

namespace KeyManagement.Api.Services;

public class KeyManagementService : IKeyManagementService
{
    private readonly CryptoProvider _cryptoProvider;
    private readonly List<Dictionary<string, SigningKey>> _keys;

    public KeyManagementService()
    {
        _cryptoProvider = new CryptoProvider();
        _keys = new List<Dictionary<string, SigningKey>>();
    }

    public async Task<SigningKey> CreateKeyAsync()
    {
        var kid = $"store-{ DateTimeOffset.UtcNow.ToUnixTimeSeconds() }";

        var publicKey = _cryptoProvider.GetPublicKey(kid);

        var privateKey  = _cryptoProvider.GetPrivateKey(kid);

        var signingKey = new SigningKey
        {
            PublicKey = publicKey,
            PrivateKey = privateKey
        };

        var keyStore = new Dictionary<string, SigningKey>
        {
            { kid, signingKey }
        };
        _keys.Add(keyStore);

        return await Task.FromResult(signingKey);
    }

    public async Task<SigningKey> GetKeyAsync(string kid)
    {
        var keyStore = _keys?.FirstOrDefault(x => x.ContainsKey(kid));

        if (keyStore == null)
            return null;

        var signingKey = keyStore[kid];

        return await Task.FromResult(signingKey);
    }
}