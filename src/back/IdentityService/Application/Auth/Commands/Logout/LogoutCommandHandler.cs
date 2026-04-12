using IdentityService.Application.Common.Interfaces;
using IdentityService.Application.Common.Models;
using IdentityService.Domain.Interfaces;
using MediatR;

namespace IdentityService.Application.Auth.Commands.Logout;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;

    public LogoutCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        ITokenService tokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var hash = _tokenService.HashToken(request.RefreshToken);
        var stored = await _refreshTokenRepository.GetByHashAsync(hash);

        if (stored is null || !stored.IsValid())
            return Result.Failure("Refresh token inválido ou já revogado.");

        stored.Revoke();
        await _refreshTokenRepository.RevokeAsync(stored);

        return Result.Success();
    }
}
