using IdentityService.Application.Common.Models;
using MediatR;

namespace IdentityService.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string Name, string Email) : IRequest<Result>;
