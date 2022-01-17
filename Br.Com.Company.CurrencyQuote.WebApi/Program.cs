using System.IO;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Infraestructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Br.Com.Company.CurrencyQuote.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.MigrateDbContextAsync<DataContext>();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureAppConfiguration((ctx, builder) =>
                        builder
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddEnvironmentVariables()
                    )
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>();
                });
    }
}
