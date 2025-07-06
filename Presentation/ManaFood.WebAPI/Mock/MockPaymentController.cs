using Microsoft.AspNetCore.Mvc;
using ManaFood.WebAPI.Mock;

namespace ManaFood.WebAPI.Controllers.Mock;

[ApiController]
[Route("api/mock/payment")]
public class MockPaymentController : ControllerBase
{
    private readonly MockPaymentSender _sender;

    public MockPaymentController(MockPaymentSender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> SendMock([FromQuery] Guid orderId)
    {
        await _sender.SendMockPayment(orderId);
        return Ok("Mock enviado!");
    }
}