using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.ItemUseCase.Commands.CreateItem;
using ManaFood.Application.UseCases.ItemUseCase.Queries.GetItemById;
using ManaFood.Application.UseCases.ItemUseCase.Queries.GetAllItems;
using ManaFood.Application.UseCases.ItemUseCase.Commands.UpdateItem;
using ManaFood.Application.UseCases.ItemUseCase.Commands.DeleteItem;

namespace ManaFood.WebAPI.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ItemDto>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllItemsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetItemByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Create(CreateItemCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> Update(Guid id, UpdateItemCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Incompatibilidade de ID entre URL e corpo da solicitação");

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, DeleteItemCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Incompatibilidade de ID entre URL e corpo da solicitação");

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
