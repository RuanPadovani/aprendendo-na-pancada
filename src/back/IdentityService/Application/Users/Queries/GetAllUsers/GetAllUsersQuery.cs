using Application.Common.Mediator;
using IdentityService.Application.Users.DTOs;

namespace IdentityService.Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>;
