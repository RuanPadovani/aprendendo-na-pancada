using IdentityService.Application.Common.Models;
using MediatR;

namespace IdentityService.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Name, string Email, string Password) : IRequest<Result<Guid>>;
