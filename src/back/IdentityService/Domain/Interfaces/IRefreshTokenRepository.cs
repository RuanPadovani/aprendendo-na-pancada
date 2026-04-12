using Domain.Entities;

namespace IdentityService.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByHashAsync(string tokenHash);
    Task SaveAsync(RefreshToken token);
    Task RevokeAsync(RefreshToken token);
}
