using IdentityService.Application.Common.Models;
using MediatR;

namespace IdentityService.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : IRequest<Result>;
