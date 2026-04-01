namespace Domain.Entities;

public sealed class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string TokenHash { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public bool Revoked { get; private set; }
    public DateTime CreatedAt { get; private set; }

    /// <summary>Construtor privado so para factory.</summary>
    private RefreshToken() { }

    public static RefreshToken Create(
        Guid userId,
        string tokenHash,
        int daysToExpired = 7)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = DateTime.UtcNow.AddDays(daysToExpired),
            Revoked = false,
            CreatedAt = DateTime.UtcNow
        };

    }

    public bool IsValid() => !Revoked && ExpiresAt > DateTime.UtcNow;

    public void Revoke()
    {
        if (Revoked)
            throw new InvalidOperationException("Token ja foi revogado.");

        Revoked = true;
    }

    public static RefreshToken Reconstitute(
    Guid id, Guid userId, string tokenHash,
    DateTime expiresAt, bool revoked, DateTime createdAt)
    {
        return new RefreshToken
        {
            Id = id,
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = expiresAt,
            Revoked = revoked,
            CreatedAt = createdAt
        };
    }
}