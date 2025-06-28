using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.OrderUseCase.Queries.GetAllOrders;

public sealed record GetAllOrdersQuery : IRequest<List<OrderDto>>;
