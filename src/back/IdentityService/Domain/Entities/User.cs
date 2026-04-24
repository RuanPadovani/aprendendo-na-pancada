using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime CreateAt { get; private set; }
    public bool IsActive { get; private set; }
    public Role Role { get; private set; }

    public User(string name, string email, string passwordHash, Role role = Role.User)
    {
        UserId = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreateAt = DateTime.UtcNow;
        IsActive = true;
        Role = role;
    }

    public User(
        Guid userId,
        string name,
        string email,
        string passwordHash,
        DateTime createAt,
        bool isActive, 
        Role role)
    {
        UserId = userId;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreateAt = createAt;
        IsActive = isActive;
        Role = role;
    }
}
