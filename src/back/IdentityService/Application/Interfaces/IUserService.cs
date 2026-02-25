using IdentityService.Application.Commands;
using IdentityService.Application.Results;

namespace IdentityService.Application.Interfaces;

public interface IUserService
{
    Task<UserResult?> GetUserByIdAsync(Guid id);
    Task <IEnumerable<UserResult>> ListAllUsersAsync();
    Task<bool> CreateUserAsync(CreateUserCommand createUser);
    Task<bool> EditUserAsync(UpdateUserCommand updateUser);
    Task<bool> DeleteUserAsync(Guid id);
}
