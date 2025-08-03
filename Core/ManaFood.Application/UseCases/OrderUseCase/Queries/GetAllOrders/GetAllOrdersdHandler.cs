using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.Interfaces;
using ManaFood.Domain.Entities;
using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.Queries.GetAllOrders;
public class GetAllOrdersdHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetAllOrdersdHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var validStatuses = new[] { OrderStatus.RECEIVED, OrderStatus.PREPARING, OrderStatus.READY };
        var orders = await _repository.GetAllActive(
            or => validStatuses.Contains(or.OrderStatus),
            cancellationToken
        );

        var sortedOrders = SortByStatusAndDate(orders);
        return _mapper.Map<List<OrderDto>>(sortedOrders);
    }

    private static List<Order> SortByStatusAndDate(List<Order> orders)
    {
        var statusOrder = new Dictionary<OrderStatus, int>
        {
            { OrderStatus.READY, 0 }, // Pronto
            { OrderStatus.PREPARING, 1 }, // Em Preparação
            { OrderStatus.RECEIVED, 2 }  // Recebido
        };

        return orders
            .Where(o => statusOrder.ContainsKey(o.OrderStatus))
            .OrderBy(o => statusOrder[o.OrderStatus])
            .ThenBy(o => o.CreatedAt)
            .ToList();
    }
}