namespace MicroservicesProject.Application.Results;

public record UserResult(
    Guid UserId,
    string Name,
    string Email,
    DateTime CreatedAt,
    bool IsActive
);