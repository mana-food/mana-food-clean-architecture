using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryWithIdCommand, CategoryDto>
{
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryWithIdCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetBy(c => c.Id == request.Id && !c.Deleted, cancellationToken);
        
        if (category == null)
            throw new ArgumentException($"Categoria com ID {request.Id} não encontrada");


        category.Name = request.Name;
        category.UpdatedAt = DateTime.UtcNow;

        await _repository.Update(category, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<CategoryDto>(category);
    }
}
