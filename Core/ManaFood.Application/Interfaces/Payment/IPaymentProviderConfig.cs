namespace ManaFood.Application.Interfaces
{
    public interface IPaymentProviderConfig
    {
        string AccessToken { get; }
        string NotificationUrl { get; }
    }
}