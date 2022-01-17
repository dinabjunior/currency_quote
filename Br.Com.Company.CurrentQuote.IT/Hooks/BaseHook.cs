using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Br.Com.Company.CurrentQuote.IT.Hooks
{
    public abstract class BaseHook
    {
        protected static readonly IConfigurationRoot Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json", false, false).Build();
        protected static NpgsqlConnection CreateConnection() => new NpgsqlConnection(Configuration.GetConnectionString("global"));
    }
}
