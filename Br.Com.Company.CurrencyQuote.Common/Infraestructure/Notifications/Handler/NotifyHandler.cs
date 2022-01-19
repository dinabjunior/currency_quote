using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications.Handler
{
    internal class NotifyHandler : INotificationHandler<Notification>
    {
        private List<Notification> _internalNotifications;

        private List<Notification> InternalNotifications => _internalNotifications ??= new List<Notification>();

        public Task Handle(Notification message, CancellationToken cancellationToken)
        {
            InternalNotifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual IReadOnlyCollection<Notification> Notifications => InternalNotifications.Where(not => not.GetType() == typeof(Notification)).ToList();

        public virtual bool HasNotifications() => _internalNotifications?.Any() == true;

        public void Dispose()
        {
            _internalNotifications = null;
        }
    }
}
