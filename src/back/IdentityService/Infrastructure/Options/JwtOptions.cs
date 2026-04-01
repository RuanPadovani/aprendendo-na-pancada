namespace Infrastructure.Options;

public sealed class JwtOptions
{
    public const string  SectionName = "JWT";
    public string  Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string SigningKey { get; init; } = default!; // em DEV via user-secrets / em PROD via env/vault
    
    public int AccessTokenMinutes { get; init; } = 15;
}