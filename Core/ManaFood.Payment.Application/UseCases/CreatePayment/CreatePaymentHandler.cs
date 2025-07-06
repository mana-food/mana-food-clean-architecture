public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, string>
{
    private readonly IPaymentService _paymentService;

    public CreatePaymentHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var qr = await _paymentService.CreatePaymentAsync(request.Amount, request.OrderId);
        return qr;
    }
}