using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Interfaces;
using System;
using System.Threading.Tasks;

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

        [HttpPost("{orderId}")]
        public async Task<IActionResult> CreatePayment(Guid orderId)
        {
            var paymentId = await _paymentService.CreatePaymentAsync(orderId, 100.00m);
            return Ok(new { PaymentId = paymentId });
        }
    }
}