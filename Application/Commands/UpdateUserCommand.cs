namespace MicroservicesProject.Application.Commands;

public record class UpdateUserCommand(Guid UserId, string Name, string Email);



