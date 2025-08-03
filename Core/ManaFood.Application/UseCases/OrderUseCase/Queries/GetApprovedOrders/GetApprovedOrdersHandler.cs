using AutoMapper;
using MediatR;
using ManaFood.Application.Dtos;
using ManaFood.Application.Interfaces;
using ManaFood.Domain.Enums;

namespace ManaFood.Application.UseCases.OrderUseCase.Queries.GetApprovedOrders;

public class GetApprovedOrdersHandler : IRequestHandler<GetApprovedOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetApprovedOrdersHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetApprovedOrdersQuery request, CancellationToken cancellationToken)
    {
        var approvedOrders = await _orderRepository.GetApprovedOrdersAsync(cancellationToken);

        return _mapper.Map<IEnumerable<OrderDto>>(approvedOrders);
    }
}