using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.CategoryUseCase.Queries.GetCategoryById;
public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(request.Id, cancellationToken);
        return _mapper.Map<CategoryDto>(category);
    }
}