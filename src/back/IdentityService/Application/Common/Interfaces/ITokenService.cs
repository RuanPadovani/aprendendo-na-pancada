using IdentityService.Domain.Enums;

namespace IdentityService.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string name, string email, Role role);
    string GenerateRefreshToken();
    string HashToken(string token);
    int GetAccessTokenExpirationInSeconds();
}
