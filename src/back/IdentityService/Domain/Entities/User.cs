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
        SetName(name);
        SetEmail(email);

        UserId = Guid.NewGuid();
        CreateAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void Update(string name, string email)
    {
        SetName(name);
        SetEmail(email);
    }

    public void SetActive(bool isActive)
        => IsActive = isActive;

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome inválido.", nameof(name));

        Name = name.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email inválido.", nameof(email));

        Email = email.Trim();
    }
}
