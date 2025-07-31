using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Interfaces;
using ManaFood.WebAPI.Controllers;
using System;
using System.Threading.Tasks;
using ManaFood.Application.Dtos;

namespace ManaFood.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentClientController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentClientController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var paymentId = await _paymentService.CreatePaymentAsync(
                request.OrderId,
                request.Amount,
                request.PayerEmail,
                request.PayerFirstName,
                request.PayerLastName,
                request.PayerId
            );

            return Ok(new { PaymentId = paymentId });
        }
    }
}