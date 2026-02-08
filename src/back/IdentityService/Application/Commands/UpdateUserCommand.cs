namespace IdentityService.Application.Commands;

public record class UpdateUserCommand(Guid UserId, string Name, string Email);



