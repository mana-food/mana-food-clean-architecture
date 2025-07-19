using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.CreateCategory;
using ManaFood.Application.UseCases.CategoryUseCase.Queries.GetCategoryById;
using ManaFood.Application.UseCases.CategoryUseCase.Queries.GetAllCategories;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.UpdateCategory;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.DeleteCategory;
using ManaFood.WebAPI.Filters;

namespace ManaFood.WebAPI.Controllers
{
    [CustomAuthorize]
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetAll([FromQuery] GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> Update(Guid id, UpdateCategoryCommand commandBody, CancellationToken cancellationToken)
        {
            var command = new UpdateCategoryWithIdCommand(id, commandBody.Name);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
