using Application.Common.Mediator;
using IdentityService.Application.Common.Interfaces;
using IdentityService.Application.Common.Models;
using IdentityService.Domain.Enums;
using IdentityService.Domain.Interfaces;

namespace IdentityService.Application.Users.Commands.CreateUser;
public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _passwordHasher.Hash(request.Password);
        var userId = await _userRepository.CreateUser(request.Name, request.Email, passwordHash, Role.User);

        if (userId is null)
            return Result<Guid>.Failure("Erro ao criar usuário.");

        return Result<Guid>.Success(userId.Value);
    }
}
