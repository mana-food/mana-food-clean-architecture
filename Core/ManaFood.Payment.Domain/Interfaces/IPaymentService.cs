using System.Threading.Tasks;

namespace ManaFood.Payment.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<string> GenerateQrCodeAsync(ManaFood.Payment.Domain.Entities.Payment payment);
    }
}
