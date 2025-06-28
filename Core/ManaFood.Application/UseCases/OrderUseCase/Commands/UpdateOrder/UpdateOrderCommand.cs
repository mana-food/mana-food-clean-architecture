using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.OrderUseCase.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(
    Guid Id,
    int OrderStatus) : IRequest<OrderDto>;