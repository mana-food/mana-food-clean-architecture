using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.Interfaces;
using ManaFood.Application.Shared;
using MediatR;

namespace ManaFood.Application.UseCases.CategoryUseCase.Queries.GetAllCategories;
public class GetAllCategoriesdHandler : IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCategoriesdHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var pagedCategories = await _repository.GetAllPaged(request.Page, request.PageSize, cancellationToken);
        var categoriesDto = _mapper.Map<List<CategoryDto>>(pagedCategories.Data);

        return new PagedResult<CategoryDto>
        {
            Data = categoriesDto,
            TotalCount = pagedCategories.TotalCount,
            PageSize = request.PageSize,
            Page = request.Page
        };
    }
}