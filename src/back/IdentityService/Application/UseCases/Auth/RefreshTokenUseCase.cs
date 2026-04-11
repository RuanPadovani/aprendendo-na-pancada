using Application.Interfaces;
using Application.UseCases.Auth.Commands;
using Application.UseCases.Auth.Results;
using Domain.Entities;
using IdentityService.Application.Interfaces;

namespace Application.UseCases.Auth;

public sealed class RefreshTokenUseCase
{
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public RefreshTokenUseCase(
        IRefreshTokenRepository refreshTokenRepo,
        IUserRepository userRepo,
        ITokenService tokenService
    )
    {
        _refreshTokenRepo = refreshTokenRepo;
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    public async Task<LoginResult> ExecuteAsync(RefreshTokenCommand command)
    {

        var hash = _tokenService.HashToken(command.RefreshToken);

        var stored = await _refreshTokenRepo.GetByHashAsync(hash);

        if (stored is null || !stored.IsValid())
            throw new UnauthorizedAccessException("Refresh token inválido ou expirado.");

        var user = await _userRepo.GetUserById(stored.UserId);
        if (user is null)
            throw new UnauthorizedAccessException("Usuário não encontrado.");

        stored.Revoke();
        await _refreshTokenRepo.RevokeAsync(stored);

        var newRawToken = _tokenService.GenerateRefreshToken();
        var newHash = _tokenService.HashToken(newRawToken);

        var newRefreshToken = RefreshToken.Create(user.UserId, newHash);
        await _refreshTokenRepo.SaveAsync(newRefreshToken);

        var accessToken = _tokenService.GenerateAccessToken(user.UserId, user.Name, user.Email);

        return new LoginResult(
            AccessToken: accessToken,
            RefreshToken: newRawToken,
            ExpiredInSeconds: _tokenService.GetAccessTokenExpirationInSeconds()
        );
    }
}
