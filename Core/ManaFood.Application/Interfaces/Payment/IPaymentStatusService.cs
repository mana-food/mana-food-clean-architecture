namespace ManaFood.Application.Interfaces
{
    public interface IPaymentStatusService
    {
        Task<(string status, string orderId)> GetPaymentStatusAsync(string paymentId);
    }
}