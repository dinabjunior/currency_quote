using Br.Com.Company.CurrencyQuote.Data.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddHttpClient();
            services.AddScoped<IRepository, GenericRepository<TContext>>();
            services.AddMediatR(typeof(IRepository));

            return services;
        }
    }
}
