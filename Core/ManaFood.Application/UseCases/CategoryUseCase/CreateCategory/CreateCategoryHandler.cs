using AutoMapper;
using ManaFood.Domain.Entities;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.CategoryUseCase.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
{
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {

        var category = _mapper.Map<Category>(request);

        await _repository.Create(category, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<CreateCategoryResponse>(category);
    }
}
