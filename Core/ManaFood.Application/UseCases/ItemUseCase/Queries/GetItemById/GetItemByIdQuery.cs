using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.ItemUseCase.Queries.GetItemById;

public sealed record GetItemByIdQuery(Guid Id) : IRequest<ItemDto>;
