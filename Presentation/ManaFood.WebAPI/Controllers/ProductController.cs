using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.ProductUseCase.Commands.CreateProduct;
using ManaFood.Application.UseCases.ProductUseCase.Queries.GetProductById;
using ManaFood.Application.UseCases.ProductUseCase.Queries.GetAllProducts;
using ManaFood.Application.UseCases.ProductUseCase.Commands.UpdateProduct;
using ManaFood.Application.UseCases.ProductUseCase.Commands.DeleteProduct;
using ManaFood.Domain.Entities;
using ManaFood.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ManaFood.WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(Guid id, UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Incompatibilidade de ID entre URL e corpo da solicitação");

            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [CustomAuthorize(UserType.ADMIN, UserType.MANAGER)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, DeleteProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Incompatibilidade de ID entre URL e corpo da solicitação");

            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
