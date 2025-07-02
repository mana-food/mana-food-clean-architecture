using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.Commands.UpdateOrder;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderHandler(IOrderRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(UpdateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _repository.GetBy(
            p => p.Id == request.Id && !p.Deleted, 
            cancellationToken, 
            p => p.Products
        );
        
        if (order == null)
            throw new ArgumentException($"Pedido com ID {request.Id} não encontrado");

        order.OrderStatus = (OrderStatus)request.OrderStatus;
        order.UpdatedAt = DateTime.UtcNow;

        await _repository.Update(order, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}
