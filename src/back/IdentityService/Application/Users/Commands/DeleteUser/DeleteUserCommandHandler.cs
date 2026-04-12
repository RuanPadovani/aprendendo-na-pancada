using IdentityService.Application.Common.Models;
using IdentityService.Domain.Interfaces;
using MediatR;

namespace IdentityService.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
            return Result.Failure("Usuário não encontrado.");

        var deleted = await _userRepository.DeleteUser(request.UserId);

        return deleted
            ? Result.Success()
            : Result.Failure("Erro ao deletar usuário.");
    }
}
