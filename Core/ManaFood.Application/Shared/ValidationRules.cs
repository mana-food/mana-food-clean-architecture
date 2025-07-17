using FluentValidation;
using ManaFood.Application.Utils;

namespace ManaFood.Application.Shared;

public static class ValidationRules
{
    public static IRuleBuilderOptions<T, string> RequiredString<T>(this IRuleBuilder<T, string> ruleBuilder, string field)
    {
        return ruleBuilder
            .NotNull().WithMessage($"{field} não pode ser nulo.")
            .NotEmpty().WithMessage($"{field} não pode ser vazio.");
    }

    public static IRuleBuilderOptions<T, string> RequiredStringWithMinLength<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName, int minLength)
    {
        return ruleBuilder
            .RequiredString(fieldName)
            .MinimumLength(minLength).WithMessage($"{fieldName} precisa ter no mínimo {minLength} caracteres.");
    }

    public static IRuleBuilderOptions<T, string> RequiredStringWithLength<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName, int minLength, int maxLength)
    {
        return ruleBuilder
            .RequiredString(fieldName)
            .Length(minLength, maxLength).WithMessage($"{fieldName} precisa ter entre {minLength} e {maxLength} caracteres.");
    }

    public static IRuleBuilderOptions<T, Guid> RequiredGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder, string fieldName)
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"{fieldName} é obrigatório.")
            .Must(id => id != Guid.Empty).WithMessage($"{fieldName} deve ser um GUID válido.");
    }

    public static IRuleBuilderOptions<T, Guid?> OptionalGuid<T>(this IRuleBuilder<T, Guid?> ruleBuilder, string fieldName)
    {
        return ruleBuilder
            .Must(id => !id.HasValue || id.Value != Guid.Empty)
            .WithMessage($"{fieldName} deve ser um GUID válido quando fornecido.");
    }

    public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .RequiredString("Email")
            .EmailAddress().WithMessage("Email deve ter um formato válido.");
    }

    public static IRuleBuilderOptions<T, string> ValidCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .RequiredString("CPF")
            .Length(11).WithMessage("CPF deve ter exatamente 11 caracteres.")
            .Must(CpfUtils.IsValidCpf).WithMessage("CPF deve ser válido.");
    }

    public static IRuleBuilderOptions<T, DateOnly> ValidBirthday<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
            .Must(birthday => birthday <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Data de nascimento não pode ser no futuro.")
            .Must(BirthdayUtils.IsValidBirthday)
            .WithMessage("Usuário deve ter no mínimo 18 anos de idade.");
    }

    public static IRuleBuilderOptions<T, string> ValidDescription<T>(this IRuleBuilder<T, string> ruleBuilder, int maxLength = 500)
    {
        return ruleBuilder
            .MaximumLength(maxLength).WithMessage($"Descrição não pode exceder {maxLength} caracteres.");
    }

    public static IRuleBuilderOptions<T, string> ValidName<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName, int minLength = 3, int maxLength = 100)
    {
        return ruleBuilder
            .RequiredStringWithLength(fieldName, minLength, maxLength)
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage($"{fieldName} deve conter apenas letras e espaços.");
    }

    public static IRuleBuilderOptions<T, double> ValidPrice<T>(this IRuleBuilder<T, double> ruleBuilder, string fieldName)
    {
        return ruleBuilder
            .GreaterThan(0).WithMessage($"{fieldName} deve ser maior que zero.")
            .LessThanOrEqualTo(999999.99).WithMessage($"{fieldName} não pode exceder R$ 999.999,99.");
    }

    public static IRuleBuilderOptions<T, IEnumerable<TElement>> NotEmptyList<T, TElement>(this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder, string fieldName)
    {
        return ruleBuilder
            .NotNull().WithMessage($"{fieldName} não pode ser nulo.")
            .Must(list => list.Any()).WithMessage($"{fieldName} deve conter pelo menos um item.");
    }
    
    public static IRuleBuilderOptions<T, int> RequiredPagination<T>(this IRuleBuilder<T, int> ruleBuilder, string field)
    {
        return ruleBuilder
            .GreaterThan(0).WithMessage($"{field} deve ser maior que zero.");
    }
}
