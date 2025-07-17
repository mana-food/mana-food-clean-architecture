using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.UserUseCase.Commands.UpdateUser;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id).RequiredGuid("ID");
        RuleFor(x => x.Name).ValidName("Nome");
        RuleFor(x => x.Email).ValidEmail();
        RuleFor(x => x.Cpf).ValidCpf();
        RuleFor(x => x.Birthday).ValidBirthday();
    }
}
