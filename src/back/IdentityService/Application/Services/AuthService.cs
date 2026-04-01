using Application.Interfaces;
using Application.UseCases.Auth.Commands;
using Application.UseCases.Auth.Results;
using IdentityService.Application.Interfaces;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _token;

    public AuthService(
        IUserRepository userRepo, 
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userRepo = userRepo;
        _passwordHasher= passwordHasher;
        _token = tokenService;
    }

    public async Task<LoginResult?> LoginAsync(LoginCommand login)
    {
        var user = await _userRepo.GetUserByEmail(login.Email);

        if (user is null)
            return null;

        if(!user.IsActive)
            return null;

        var passwordIsValid = _passwordHasher.Verify(login.Password, user.PasswordHash);

        if(!passwordIsValid)
            return null;

        var accessToken = _token.GenerateAccessToken(user.UserId, user.Name, user.Email);
        var rawRefreshToken = _token.GenerateRefreshToken();
        var expiredIn = _token.GetAccessTokenExpirationInSeconds();

        return new LoginResult(accessToken, rawRefreshToken, expiredIn);

    }
}
