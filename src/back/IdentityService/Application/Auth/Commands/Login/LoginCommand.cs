using Application.Common.Mediator;
using IdentityService.Application.Auth.DTOs;
using IdentityService.Application.Common.Models;

namespace IdentityService.Application.Auth.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;
