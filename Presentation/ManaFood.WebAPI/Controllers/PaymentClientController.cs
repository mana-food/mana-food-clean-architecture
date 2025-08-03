using Microsoft.AspNetCore.Mvc;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment;
using MediatR;
using AutoMapper;
using ManaFood.Application.Interfaces;

namespace ManaFood.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        private readonly IOrderRepository _orderRepository;

        public PaymentClientController(
            IMediator mediator,
            IMapper mapper,
            IPaymentService paymentService,
            IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _paymentService = paymentService;
            _orderRepository = orderRepository;
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CreatePaymentResponse>> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var command = _mapper.Map<CreatePaymentCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("qr-image/{orderId}")]
        public async Task<IActionResult> GetQrImage(Guid orderId)
        {
            var order = await _orderRepository.GetByIdWithProductsAsync(orderId);
            if (order == null)
                return NotFound("Pedido não encontrado.");

            var response = await _paymentService.CreatePaymentAsync(orderId);

            var imageBytes = Convert.FromBase64String(response.QrCodeBase64);

            // Retorna a imagem como PNG
            return File(imageBytes, "image/png");
        }
    }
}