using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.Queries.GetOrderById;
public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrderByIdHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetBy(p => p.Id == request.Id && !p.Deleted, cancellationToken, p => p.Products);
    
        if (order is null)
            throw new ArgumentException($"Pedido com ID {request.Id} não encontrado");
        var result = _mapper.Map<OrderDto>(order);
        return result;
    }
}