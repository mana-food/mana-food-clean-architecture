using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.OrderUseCase.Queries.GetApprovedOrders;

public class GetApprovedOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}