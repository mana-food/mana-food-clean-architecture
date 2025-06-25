using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.CreateItem;

public sealed record CreateItemCommand(
    string Name,
    string? Description,
    Guid CategoryId) : IRequest<ItemDto>;