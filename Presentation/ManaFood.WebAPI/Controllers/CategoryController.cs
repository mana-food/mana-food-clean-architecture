using ManaFood.Application.UseCases.CategoryUseCase.CreateCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManaFood.WebAPI.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCategoryResponse>> Create(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request);
            return Created();
        }
    }
}
