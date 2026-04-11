using Application.Interfaces;
using Application.UseCases.Auth.Commands;
using Application.UseCases.Auth.Results;
using Domain.Entities;
using IdentityService.Application.Interfaces;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _token;
    private readonly IRefreshTokenRepository _refreshTokenRepo;

    public AuthService(
        IUserRepository userRepo,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepo)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
        _token = tokenService;
        _refreshTokenRepo = refreshTokenRepo;
    }

    public async Task<LoginResult?> LoginAsync(LoginCommand login)
    {
        var user = await _userRepo.GetUserByEmail(login.Email);

        if (user is null)
            return null;

        if (!user.IsActive)
            return null;

        var passwordIsValid = _passwordHasher.Verify(login.Password, user.PasswordHash);

        if (!passwordIsValid)
            return null;

        var accessToken = _token.GenerateAccessToken(user.UserId, user.Name, user.Email);
        var rawRefreshToken = _token.GenerateRefreshToken();
        var tokenHash = _token.HashToken(rawRefreshToken);
        var expiredIn = _token.GetAccessTokenExpirationInSeconds();

        var refreshToken = RefreshToken.Create(user.UserId, tokenHash);
        await _refreshTokenRepo.SaveAsync(refreshToken);

        return new LoginResult(accessToken, rawRefreshToken, expiredIn);
    }
}
