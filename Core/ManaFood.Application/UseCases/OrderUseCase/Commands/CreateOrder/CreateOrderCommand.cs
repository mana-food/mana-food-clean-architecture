using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.OrderUseCase.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    int PaymentMethod,
    List<ProductOrderDto> Products) : IRequest<OrderDto>;