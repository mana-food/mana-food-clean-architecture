using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.OrderUseCase.Commands.CreateOrder;
using ManaFood.Application.UseCases.OrderUseCase.Queries.GetOrderById;
using ManaFood.Application.UseCases.OrderUseCase.Queries.GetAllOrders;
using ManaFood.Application.UseCases.OrderUseCase.Commands.UpdateOrder;
using ManaFood.Application.UseCases.OrderUseCase.Commands.DeleteOrder;
using ManaFood.Application.UseCases.OrderUseCase.Queries.GetApprovedOrders;

namespace ManaFood.WebAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("ready")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetReadyForKitchen(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetApprovedOrdersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> Update(Guid id, UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Incompatibilidade de ID entre URL e corpo da solicitação");

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Incompatibilidade de ID entre URL e corpo da solicitação");

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
