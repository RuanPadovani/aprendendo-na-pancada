namespace IdentityService.Application.Users.DTOs;

public sealed record UserResponse(
    Guid UserId,
    string Name,
    string Email,
    DateTime CreatedAt,
    bool IsActive
);
