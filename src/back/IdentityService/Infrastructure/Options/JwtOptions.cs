namespace Infrastructure.Options;

public sealed class JwtOptions
{
    public const string SectionName = "JWT";
    public string ValidIssuer { get; init; } = default!;
    public string ValidAudiences { get; init; } = default!;
    public string SecretKey { get; init; } = default!;

    public int TokenValidMinutes { get; init; } = 15;
}