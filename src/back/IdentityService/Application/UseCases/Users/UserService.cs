using IdentityService.Application.Commands;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Results;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.UseCases;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
        => _userRepository = userRepository;


    public async Task<UserResult?> GetUserById(Guid id)
    {
        var userEntity = await _userRepository.GetUserById(id);

        return userEntity is null ? null : ToResult(userEntity);
    }

    public async Task<IEnumerable<UserResult>> ListAllUsers()
    {
        var users = await _userRepository.GetAll();
        return users.Select(ToResult);
    }

    public async Task<Guid>CreateUser(CreateUserCommand command)
    {
        var userEntity = new User(
            name: command.Name,
            email: command.Email
        );

        await _userRepository.CreateUser(userEntity);

        return userEntity.UserId;
    }

    public async Task EditUser(UpdateUserCommand command)
    {
        var existing = await _userRepository.GetUserById(command.UserId);
        if (existing is null) return;

        existing.Update(command.Name, command.Email);

        await _userRepository.EditUser(existing);
    }

    public async Task DeleteUser(Guid id)
    {
        var userEntity = await _userRepository.GetUserById(id);
        if (userEntity is null) return;

        await _userRepository.DeleteUser(userEntity.UserId);
    }

    private static UserResult ToResult(User user)
        => new(
            user.UserId,
            user.Name,
            user.Email,
            user.CreateAt,
            user.IsActive
        );
}
