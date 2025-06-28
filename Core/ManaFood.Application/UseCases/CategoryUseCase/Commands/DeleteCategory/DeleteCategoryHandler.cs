using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetBy(c => c.Id == request.Id && !c.Deleted, cancellationToken);

        if (category == null)
            throw new ArgumentException($"Categoria com ID {request.Id} não encontrada");


        category.Deleted = true;
        category.UpdatedAt = DateTime.UtcNow;

        await _repository.Delete(category, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
