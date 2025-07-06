using Microsoft.AspNetCore.Mvc;
using MediatR;
using ManaFood.Application.UseCases.OrderUseCase.ConfirmPayment;

namespace ManaFood.WebAPI.Webhooks.MercadoPago;

[ApiController]
[Route("api/webhooks/mercadopago")]
public class MercadoPagoWebhookReceiver : ControllerBase
{
    private readonly IMediator _mediator;

    public MercadoPagoWebhookReceiver(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("payment-confirmation")]
    public async Task<IActionResult> ReceivePaymentConfirmation([FromBody] MercadoPagoWebhookPayload payload)
    {
        if (payload == null || payload.Data == null || string.IsNullOrEmpty(payload.Data.Id))
        {
            return BadRequest("Invalid payload");
        }

        var command = new ConfirmPaymentCommand(payload.Data.Id);
        await _mediator.Send(command);

        return Ok();
    }
} 