using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.CreateCategory;
using ManaFood.Application.UseCases.CategoryUseCase.Queries.GetCategoryById;
using ManaFood.Application.UseCases.CategoryUseCase.Queries.GetAllCategories;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.UpdateCategory;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.DeleteCategory;
using ManaFood.Domain.Entities;
using ManaFood.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ManaFood.WebAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetAll([FromQuery] GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> Update(Guid id, UpdateCategoryCommand commandBody, CancellationToken cancellationToken)
        {
            var command = new UpdateCategoryWithIdCommand(id, commandBody.Name);
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
