namespace Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications
{
    public interface INotify
    {
        bool HasNotifications { get; }

        void AddNotification(string key, string message);
    }
}
