using MediatR;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.DeleteItem;

public sealed record DeleteItemCommand(Guid Id) : IRequest<Unit>;