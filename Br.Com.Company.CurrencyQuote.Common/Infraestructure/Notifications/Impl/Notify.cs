using System.Threading;
using Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications.Handler;
using MediatR;

namespace Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications.Impl
{
    internal class Notify : INotify
    {
        private readonly NotifyHandler _messageHandler;

        public Notify(INotificationHandler<Notification> notification)
        {
            _messageHandler = (NotifyHandler)notification;
        }

        public bool HasNotifications => !_messageHandler.HasNotifications();

        public void AddNotification(string key, string message)
        {
            _messageHandler.Handle(new Notification(key, message), default(CancellationToken));
        }
    }
}
