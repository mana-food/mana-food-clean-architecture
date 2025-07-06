using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManaFood.Payment.Application.UseCases.CreatePayment;

namespace ManaFood.PaymentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GeneratePayment([FromBody] CreatePaymentCommand command)
        {
            var qrUrl = await _mediator.Send(command);
            return Ok(new { qrCode = qrUrl });
        }
    }
}