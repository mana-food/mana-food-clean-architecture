using Microsoft.AspNetCore.Mvc;
using ManaFood.Payment.Domain.Entities;
using ManaFood.Payment.Domain.Interfaces;

namespace ManaFood.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentClientController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentClientController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GeneratePaymentLink([FromBody] ManaFood.Payment.Domain.Entities.Payment payment)

    {
        try
        {
            var link = await _paymentService.GenerateQrCodeAsync(payment);
            return Ok(new { paymentUrl = link });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro ao gerar QR Code", error = ex.Message });
        }
    }
}