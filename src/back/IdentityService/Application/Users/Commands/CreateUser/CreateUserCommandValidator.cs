using FluentValidation;

namespace IdentityService.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Password)    
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(6).WithMessage("Senha deve ter pelo menos 6 caracteres.")
            .Matches(@"[A-Z]").WithMessage("Deve conter uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("Deve conter uma letra minúscula.")
            .Matches(@"[\d]").WithMessage("Deve conter um número.");
    }
}
