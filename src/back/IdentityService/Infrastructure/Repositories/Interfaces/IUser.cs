using IdentityService.Domain.Entities;

namespace Infrastructure.Repositories.Interfaces;

internal interface IUser
{
    public Task<IEnumerable<User>> ListAllUser();
}
