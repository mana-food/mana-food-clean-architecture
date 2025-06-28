using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.OrderUseCase.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto>;
