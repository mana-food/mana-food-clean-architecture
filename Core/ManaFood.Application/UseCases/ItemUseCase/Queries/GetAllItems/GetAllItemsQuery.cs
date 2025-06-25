using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.ItemUseCase.Queries.GetAllItems;

public sealed record GetAllItemsQuery : IRequest<List<ItemDto>>;
