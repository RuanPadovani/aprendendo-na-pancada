namespace MicroservicesProject.Application.Commands;

public record SetUserActiveCommand(Guid Id, bool IsActive);
