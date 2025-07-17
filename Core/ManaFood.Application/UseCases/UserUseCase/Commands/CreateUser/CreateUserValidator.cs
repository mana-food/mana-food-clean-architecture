using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.UserUseCase.Commands.CreateUser;

public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).ValidName("Nome");
        RuleFor(x => x.Email).ValidEmail();
        RuleFor(x => x.Cpf).ValidCpf();
        RuleFor(x => x.Birthday).ValidBirthday();
    }
}
