using Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications;
using Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications.Handler;
using Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications.Impl;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            // Notifications
            services.AddScoped<INotificationHandler<Notification>, NotifyHandler>();
            services.AddScoped<INotify, Notify>();

            return services;
        }
    }
}
