using IdentityService.Application.Auth.DTOs;
using RefreshTokenEntity = Domain.Entities.RefreshToken;
using IdentityService.Application.Common.Interfaces;
using IdentityService.Application.Common.Models;
using IdentityService.Domain.Interfaces;
using Application.Common.Mediator;

namespace IdentityService.Application.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var hash = _tokenService.HashToken(request.RefreshToken);
        var stored = await _refreshTokenRepository.GetByHashAsync(hash);

        if (stored is null || !stored.IsValid())
            return Result<LoginResponse>.Failure("Refresh token inválido ou expirado.");

        var user = await _userRepository.GetUserById(stored.UserId);
        if (user is null)
            return Result<LoginResponse>.Failure("Usuário não encontrado.");

        stored.Revoke();
        await _refreshTokenRepository.RevokeAsync(stored);

        var newRawToken = _tokenService.GenerateRefreshToken();
        var newHash = _tokenService.HashToken(newRawToken);
        var newRefreshToken = RefreshTokenEntity.Create(user.UserId, newHash);
        await _refreshTokenRepository.SaveAsync(newRefreshToken);

        var accessToken = _tokenService.GenerateAccessToken(user.UserId, user.Name, user.Email);
        var expiredIn = _tokenService.GetAccessTokenExpirationInSeconds();

        return Result<LoginResponse>.Success(new LoginResponse(accessToken, newRawToken, expiredIn));
    }
}
