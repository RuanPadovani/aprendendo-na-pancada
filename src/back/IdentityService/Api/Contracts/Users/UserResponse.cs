namespace IdentityService.API.Contracts.Users;

public record UserResponse(
    Guid UserId,
    string Name,
    string Email,
    DateTime CreateAt,
    bool IsActive
);
