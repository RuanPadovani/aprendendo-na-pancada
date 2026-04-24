using Application.Common.Mediator;
using IdentityService.Application.Common.Models;
using IdentityService.Domain.Enums;

namespace IdentityService.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Name, string Email, string Password, Role role) : IRequest<Result<Guid>>;
