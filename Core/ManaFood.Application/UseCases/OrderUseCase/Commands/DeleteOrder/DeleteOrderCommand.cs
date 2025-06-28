using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(Guid Id) : IRequest<Unit>;