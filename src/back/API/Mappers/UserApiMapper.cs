using MicroservicesProject.API.Contracts.Users;
using MicroservicesProject.Application.Commands;
using MicroservicesProject.Application.Results;

namespace MicroservicesProject.API.Mappers;

public static class UserApiMapper
{
    public static CreateUserCommand ToCommand(this CreateUserRequest req)
        => new (
            req.Name, 
            req.Email);

    public static UpdateUserCommand ToCommand(this UpdateUserRequest req, Guid UserId)
        => new (UserId, req.Name, req.Email);

    public static UserResponse ToResponse(this UserResult result)
        => new (result.UserId, result.Name, result.Email, result.CreatedAt, result.IsActive);   
}
