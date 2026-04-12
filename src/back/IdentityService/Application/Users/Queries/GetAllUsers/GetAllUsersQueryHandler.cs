using IdentityService.Application.Users.DTOs;
using IdentityService.Domain.Interfaces;
using MediatR;

namespace IdentityService.Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.ListAllUser();

        return users.Select(u => new UserResponse(u.UserId, u.Name, u.Email, u.CreateAt, u.IsActive));
    }
}
