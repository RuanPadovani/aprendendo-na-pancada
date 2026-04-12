using IdentityService.Application.Auth.DTOs;
using RefreshTokenEntity = Domain.Entities.RefreshToken;
using IdentityService.Application.Common.Interfaces;
using IdentityService.Application.Common.Models;
using IdentityService.Domain.Interfaces;
using MediatR;

namespace IdentityService.Application.Auth.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);

        if (user is null || !user.IsActive)
            return Result<LoginResponse>.Failure("Email ou senha inválidos.");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result<LoginResponse>.Failure("Email ou senha inválidos.");

        var accessToken = _tokenService.GenerateAccessToken(user.UserId, user.Name, user.Email);
        var rawRefreshToken = _tokenService.GenerateRefreshToken();
        var tokenHash = _tokenService.HashToken(rawRefreshToken);
        var expiredIn = _tokenService.GetAccessTokenExpirationInSeconds();

        var refreshToken = RefreshTokenEntity.Create(user.UserId, tokenHash);
        await _refreshTokenRepository.SaveAsync(refreshToken);

        return Result<LoginResponse>.Success(new LoginResponse(accessToken, rawRefreshToken, expiredIn));
    }
}
