using IdentityService.Application.Results;
using Domain.Entities;

namespace IdentityService.Application.Mappers;
public static class UserMappers
{
    public static UserResult ToResult (User user)
        => new (user.UserId, user.Name, user.Email, user.CreateAt, user.IsActive);
}
