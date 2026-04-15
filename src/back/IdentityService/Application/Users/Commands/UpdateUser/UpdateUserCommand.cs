using Application.Common.Mediator;
using IdentityService.Application.Common.Models;

namespace IdentityService.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string Name, string Email) : IRequest<Result>;
