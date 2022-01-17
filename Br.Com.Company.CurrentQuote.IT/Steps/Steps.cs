using System.Net.Http;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Infraestructure.Data;
using Br.Com.Company.CurrentQuote.IT.Support.Extensions;
using TechTalk.SpecFlow;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Br.Com.Company.CurrentQuote.IT.Steps
{
    [Binding]
    public partial class Steps : IClassFixture<WebApiFactory>
    {
        private readonly WebApiFactory _factory;
        private readonly ScenarioContext _scenarioContext;
        private HttpClient HttpClient;

        public Steps(WebApiFactory factory, ScenarioContext scenarioContext)
        {
            _factory = factory;
            _scenarioContext = scenarioContext;
            HttpClient = _factory.CreateClient();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await _factory.MigrateDbContextAsync<DataContext>();
        }
    }
}
