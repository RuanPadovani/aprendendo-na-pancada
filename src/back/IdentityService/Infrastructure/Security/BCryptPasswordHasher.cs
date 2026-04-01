using Application.Interfaces;

namespace Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash( string password)
        => BCrypt.Net.BCrypt.HashPassword(password);


    public bool Verify(string password, string hashPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashPassword);
}