using IdentityService.Application.Users.DTOs;
using MediatR;

namespace IdentityService.Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>;
