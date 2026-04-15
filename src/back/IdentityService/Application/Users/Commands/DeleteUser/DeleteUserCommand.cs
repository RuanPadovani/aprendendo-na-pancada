using Application.Common.Mediator;
using IdentityService.Application.Common.Models;

namespace IdentityService.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : IRequest<Result>;
