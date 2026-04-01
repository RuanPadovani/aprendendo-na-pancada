namespace Application.Interfaces;

public interface ITokenService
{
    /// <summary>Recebe um usuario e retorna um token.</summary>
    /// <param name="user">Representa um Usuario do dominio.</param>
    string GenerateAccessToken(Guid userId, string name, string email);

    string GenerateRefreshToken();

    string HashToken(string token);

    /// <summary>Busca tokens expirados.</summary>
    int GetAccessTokenExpirationInSeconds();
}
