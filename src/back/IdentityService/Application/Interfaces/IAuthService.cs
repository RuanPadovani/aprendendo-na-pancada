using Application.UseCases.Auth.Commands;
using Application.UseCases.Auth.Results;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<LoginResult?> LoginAsync(LoginCommand login);
}
