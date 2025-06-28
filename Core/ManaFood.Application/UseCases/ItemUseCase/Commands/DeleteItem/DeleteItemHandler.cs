using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.DeleteItem;

public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, Unit>
{
    private readonly IItemRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteItemHandler(IItemRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetBy(i => i.Id == request.Id && !i.Deleted, cancellationToken);

        if (item == null)
            throw new ArgumentException($"Item com ID {request.Id} não encontrado");


        item.Deleted = true;
        item.UpdatedAt = DateTime.UtcNow;

        await _repository.Delete(item, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
