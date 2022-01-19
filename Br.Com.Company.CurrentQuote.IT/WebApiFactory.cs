using System.IO;
using Br.Com.Company.CurrencyQuote.WebApi;
using Br.Com.Company.CurrentQuote.IT.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Br.Com.Company.CurrentQuote.IT
{
    public class WebApiFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder => Configure(webBuilder));

        private static IWebHostBuilder Configure(IWebHostBuilder webHostBuilder) => 
            webHostBuilder
                .ConfigureAppConfiguration(cfg =>
                {
                    cfg.SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.test.json", false, false);
                })
                .ConfigureTestServices(cfg =>
                {
                    cfg.AddScoped<ExchangeRatesApiMock>();
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseTestServer();
    }
}
