namespace KeyManagement.Api.Models;

public record PrivateKey
(
    string kty,
    string n,
    string e,
    string d,
    string p,
    string q,
    string dp,
    string dq,
    string qi,
    string alg,
    string use,
    string kid,
    long iat,
    long exp
);