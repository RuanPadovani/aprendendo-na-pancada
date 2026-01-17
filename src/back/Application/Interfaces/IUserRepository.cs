using MicroservicesProject.Domain.Entities;

namespace MicroservicesProject.Application.Interfaces;

public interface IUserRepository
{
    Task <IEnumerable<User>> GetAll();

    Task<User> GetUserById(Guid id);

    Task<User> CreateUser(User user);

    Task<User> EditUser(User user);

    Task<User> DeleteUser(Guid id);
}
