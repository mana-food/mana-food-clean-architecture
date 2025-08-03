using ManaFood.Application.Dtos;

namespace ManaFood.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<CreatePaymentResponse> CreatePaymentAsync(Guid orderId);
    }
}