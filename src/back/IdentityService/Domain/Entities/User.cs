namespace IdentityService.Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime CreateAt { get; private set; }
    public bool IsActive { get; private set; }

    public User(string name, string email)
    {
        UserId = Guid.NewGuid();
        Name = name;
        Email = email;
        CreateAt = DateTime.UtcNow;
        IsActive = true;
    }

    public User(
        Guid userId,
        string name,
        string email,
        DateTime creaAt,
        bool isActice)
    {
        UserId = userId;
        Name = name;
        Email = email;
        CreateAt = creaAt;
        IsActive = isActice;
    }
}
