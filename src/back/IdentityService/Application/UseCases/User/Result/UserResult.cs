namespace IdentityService.Application.Results;

public sealed record UserResult(
    Guid UserId,
    string Name,
    string Email,
    DateTime CreatedAt,
    bool IsActive
);