using IdentityService.Application.Common.Models;
using MediatR;

namespace IdentityService.Application.Auth.Commands.Logout;

public sealed record LogoutCommand(string RefreshToken) : IRequest<Result>;
