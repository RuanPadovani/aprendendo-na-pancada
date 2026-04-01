using Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAllUser();

    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);

    Task<bool> CreateUser(string name, string email, string passwordHash);

    Task<bool> EditUser(Guid id, string name, string email);

    Task<bool> DeleteUser(Guid id);
    
}
