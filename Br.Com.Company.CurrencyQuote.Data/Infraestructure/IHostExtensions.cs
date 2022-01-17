using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;

namespace Microsoft.AspNetCore.Hosting
{
    public static class IHostExtensions
    {
        public static async Task<IHost> MigrateDbContextAsync<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {NAME}", typeof(TContext).Name);

                    var retry = Policy.Handle<NpgsqlException>()
                         .WaitAndRetryAsync(new TimeSpan[]
                         {
                             TimeSpan.FromSeconds(5),
                             TimeSpan.FromSeconds(10),
                             TimeSpan.FromSeconds(15),
                         });

                    await retry.ExecuteAsync(async () =>
                    {
                        await context.Database.MigrateAsync();
                    });

                    logger.LogInformation("Migrated database associated with context {NAME}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {NAME}", typeof(TContext).Name);
                }
            }

            return host;
        }
    }
}
