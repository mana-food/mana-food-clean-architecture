using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.UpdateItem;

public sealed record UpdateItemCommand(
    Guid Id,
    string Name,
    string? Description,
    Guid CategoryId) : IRequest<ItemDto>;