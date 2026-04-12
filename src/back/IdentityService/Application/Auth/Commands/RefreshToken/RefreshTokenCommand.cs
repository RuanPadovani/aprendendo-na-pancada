using IdentityService.Application.Auth.DTOs;
using IdentityService.Application.Common.Models;
using MediatR;

namespace IdentityService.Application.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<LoginResponse>>;
