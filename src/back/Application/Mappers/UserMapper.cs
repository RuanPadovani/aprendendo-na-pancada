using MicroservicesProject.Application.Results;
using MicroservicesProject.Domain.Entities;

namespace MicroservicesProject.Application.Mappers;

public static class UserMappers
{
    public static UserResult ToResult (User user)
        => new (user.UserId, user.Name, user.Email, user.CreateAt, user.IsActive);
}
