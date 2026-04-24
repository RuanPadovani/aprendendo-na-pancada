using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAllUser();
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<Guid?> CreateUser(string name, string email, string passwordHash, Role role);
    Task<bool> EditUser(Guid id, string name, string email);
    Task<bool> DeleteUser(Guid id);
}
