using IdentityService.Application.Commands;
using IdentityService.Application.Results;

namespace IdentityService.Application.Interfaces;

public interface IUserService
{
    Task<UserResult?> GetUserById(Guid id);
    Task <IEnumerable<UserResult>> ListAllUsers();
    Task<Guid> CreateUser(CreateUserCommand createUser);
    Task EditUser(UpdateUserCommand updateUser);
    Task DeleteUser(Guid id);
}
