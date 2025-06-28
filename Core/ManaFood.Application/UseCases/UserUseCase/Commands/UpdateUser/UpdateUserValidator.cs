using FluentValidation;

namespace ManaFood.Application.UseCases.UserUseCase.Commands.UpdateUser;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio.");
        RuleFor(x => x.Name).NotNull().WithMessage("Nome não pode ser nulo.");
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Nome precisa ter no mínimo 3 caracteres.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email não pode ser vazio.");
        RuleFor(x => x.Email).NotNull().WithMessage("Email não pode ser nulo.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido.");
        RuleFor(x => x.Cpf).NotEmpty().WithMessage("CPF não pode ser vazio.");
        RuleFor(x => x.Cpf).NotNull().WithMessage("CPF não pode ser nulo.");
        RuleFor(x => x.Cpf).Length(11).WithMessage("CPF precisa ter 11 caracteres.");
        RuleFor(x => x.Birthday).NotEmpty().WithMessage("Data de nascimento não pode ser vazia.");
        RuleFor(x => x.Birthday).NotNull().WithMessage("Data de nascimento não pode ser nula.");
        RuleFor(x => x.UserType).NotNull().WithMessage("Tipo de usuário não pode ser nulo.");
        RuleFor(x => x.UserType).InclusiveBetween(0, 4).WithMessage("Tipo de usuário deve ser entre 0 e 4.");
    }
}
