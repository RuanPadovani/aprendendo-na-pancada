using Application.Interfaces;
using IdentityService.Application.Commands;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Results;
using Domain.Entities;

namespace IdentityService.Application.UseCases;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }



    public async Task<IEnumerable<UserResult>> ListAllUsersAsync()
    {
        var users = await _userRepository.ListAllUser();
        return users.Select(ToResult);
    }

    public async Task<UserResult?> GetUserByIdAsync(Guid id)
    {
        var userEntity = await _userRepository.GetUserById(id);

        return userEntity is null ? null : ToResult(userEntity);
    }

    public async Task<bool> CreateUserAsync(CreateUserCommand createUser)
    {
        var passwordHash = _passwordHasher.Hash(createUser.Password);
        return await _userRepository.CreateUser(createUser.Name, createUser.Email, passwordHash);
    }

    public async Task<bool> EditUserAsync(UpdateUserCommand user)
    {
        var updateUser = await _userRepository.EditUser(user.UserId, user.Name, user.Name);

        return updateUser == true ? true : false;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var deleteUser = await _userRepository.DeleteUser(id);

        return deleteUser == true ? true : false;
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
