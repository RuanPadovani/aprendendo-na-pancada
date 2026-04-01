using IdentityService.API.Contracts.Users;
using IdentityService.Application.Commands;
using IdentityService.Application.Results;

namespace IdentityService.API.Mappers;

public static class UserApiMapper
{
    public static CreateUserCommand ToCommand(this CreateUserRequest req)
        => new (
            req.Name, 
            req.Email,
            req.Password);

    public static UpdateUserCommand ToCommand(this UpdateUserRequest req, Guid UserId)
        => new (UserId, req.Name, req.Email);

    public static UserResponse ToResponse(this UserResult result)
        => new (result.UserId, result.Name, result.Email, result.CreatedAt, result.IsActive);   
}
