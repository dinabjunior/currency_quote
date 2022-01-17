using Br.Com.Company.CurrencyQuote.Domain.Services;
using Br.Com.Company.CurrencyQuote.Domain.Services.Impl;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ISegmentRateService, SegmentRateService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IExchangeService, ExchangeService>();

            return services;
        }
    }
}
