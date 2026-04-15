using Application.Common.Mediator;
using IdentityService.Application.Common.Models;

namespace IdentityService.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Name, string Email, string Password) : IRequest<Result<Guid>>;
