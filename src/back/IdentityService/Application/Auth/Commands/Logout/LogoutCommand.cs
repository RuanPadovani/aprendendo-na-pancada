using Application.Common.Mediator;
using IdentityService.Application.Common.Models;

namespace IdentityService.Application.Auth.Commands.Logout;

public sealed record LogoutCommand(string RefreshToken) : IRequest<Result>;
