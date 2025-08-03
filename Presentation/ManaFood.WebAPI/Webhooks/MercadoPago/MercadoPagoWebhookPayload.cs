using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ManaFood.WebAPI.Webhooks.MercadoPago
{
    public class MercadoPagoWebhookPayload
    {
        public MercadoPagoData Data { get; set; }
    }

    public class MercadoPagoData
    {
        public string Id { get; set; }
    }
}