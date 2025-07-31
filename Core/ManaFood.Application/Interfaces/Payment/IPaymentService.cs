namespace ManaFood.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentAsync(Guid orderId);
    }
}