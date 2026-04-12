namespace IdentityService.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string name, string email);
    string GenerateRefreshToken();
    string HashToken(string token);
    int GetAccessTokenExpirationInSeconds();
}
