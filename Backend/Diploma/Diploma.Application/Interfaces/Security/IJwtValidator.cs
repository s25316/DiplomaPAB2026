namespace Diploma.Application.Interfaces.Security;

[Flags]
public enum JwtValidtion
{
    None = 0,
    Lifetime = 1 << 0,          // 1
    Issuer = 1 << 1,            // 2
    Audience = 1 << 2,          // 4
    IssuerSigningKey = 1 << 3,  // 8
    All = Lifetime | Issuer | Audience | IssuerSigningKey
}

public interface IJwtValidator
{
    bool IsValid(
        string token,
        JwtValidtion validtion = JwtValidtion.All);
}