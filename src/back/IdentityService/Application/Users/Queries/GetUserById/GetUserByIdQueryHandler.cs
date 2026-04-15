using Application.Common.Mediator;
using IdentityService.Application.Users.DTOs;
using IdentityService.Domain.Interfaces;

namespace IdentityService.Application.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        if (user is null)
            return null;

        return new UserResponse(user.UserId, user.Name, user.Email, user.CreateAt, user.IsActive);
    }
}
