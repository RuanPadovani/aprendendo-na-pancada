using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAllUser();

    Task<User?> GetUserById(Guid id);

    Task<bool> CreateUser(string name, string email);

    Task<bool> EditUser(Guid id, string name, string email);

    Task<bool> DeleteUser(Guid id);
    
}
