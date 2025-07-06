using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ManaFood.WebAPI.Controllers;

[ApiController]
[Route("api/webhook/payment")]
public class PaymentNotificationController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] JsonElement payload)
    {
        // Registra a notificação recebida (log, fila, etc)
        Console.WriteLine("Webhook recebido: " + payload.ToString());

        // Publicar um evento no barramento ou fila, sem lógica direta
        return Ok();
    }
}