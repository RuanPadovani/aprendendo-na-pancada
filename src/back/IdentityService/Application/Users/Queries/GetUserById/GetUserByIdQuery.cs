using Application.Common.Mediator;
using IdentityService.Application.Users.DTOs;

namespace IdentityService.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserResponse?>;
