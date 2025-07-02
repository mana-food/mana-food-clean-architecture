using ManaFood.Application.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.Commands.DeleteOrder;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _repository.GetBy(p => p.Id == request.Id && !p.Deleted, cancellationToken);

        if (order == null)
            throw new ArgumentException($"Pedido com ID {request.Id} não encontrado");


        order.Deleted = true;
        order.UpdatedAt = DateTime.UtcNow;

        await _repository.Delete(order, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
