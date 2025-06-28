using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.UserUseCase.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid Id,
    string Email,
    string Name,
    string Cpf,
    DateOnly Birthday,
    int UserType) : IRequest<UserDto>;