using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IProductRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetBy(p => p.Id == request.Id && !p.Deleted, cancellationToken);

        if (product == null)
            throw new ArgumentException($"Produto com ID {request.Id} não encontrado");


        product.Deleted = true;
        product.UpdatedAt = DateTime.UtcNow;

        await _repository.Delete(product, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
