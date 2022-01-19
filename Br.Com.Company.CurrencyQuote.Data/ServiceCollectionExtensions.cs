using Br.Com.Company.CurrencyQuote.Data.Persistence.Repository;
using Br.Com.Company.CurrencyQuote.Data.Support.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData<TContext>(this IServiceCollection services, IConfiguration config) where TContext : DbContext
        {
            services.AddHttpClient();
            services.AddScoped<IRepository, GenericRepository<TContext>>();
            services.AddMediatR(typeof(IRepository));

            services.Configure<ExchangeRatesApiOptions>(options => config.GetSection("ExchangeRatesApi").Bind(options, e => e.BindNonPublicProperties = true));

            return services;
        }
    }
}
