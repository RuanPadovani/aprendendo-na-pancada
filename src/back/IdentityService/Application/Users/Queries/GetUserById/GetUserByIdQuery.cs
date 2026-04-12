using IdentityService.Application.Users.DTOs;
using MediatR;

namespace IdentityService.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserResponse?>;
