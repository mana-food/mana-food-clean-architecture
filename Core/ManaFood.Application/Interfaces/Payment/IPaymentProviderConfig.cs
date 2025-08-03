namespace ManaFood.Application.Interfaces
{
    public interface IPaymentProviderConfig
    {
        string AccessToken { get; }
        string NotificationUrl { get; }
        long UserId { get; }
        string StoreId { get; }
        string ExternalStoreId { get; }
        string ExternalPosId { get; }
    }
}