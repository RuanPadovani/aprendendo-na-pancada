using IdentityService.Application.Common.Models;
using IdentityService.Domain.Interfaces;
using MediatR;

namespace IdentityService.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updated = await _userRepository.EditUser(request.UserId, request.Name, request.Email);

        return updated
            ? Result.Success()
            : Result.Failure("Usuário não encontrado ou erro ao atualizar.");
    }
}
