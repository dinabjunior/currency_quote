using System;
using Br.Com.Company.CurrencyQuote.Data.Support.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Br.Com.Company.CurrentQuote.IT.Mocks
{
    public class ExchangeRatesApiMock : IDisposable
    {
        private readonly WireMockServer _mockServer;

        public ExchangeRatesApiMock(IOptions<ExchangeRatesApiOptions> options)
        {
            _mockServer = WireMockServer.Start(options.Value.Url);
        }

        public void SetupExchangeRatesApi(object stubResponseObject)
        {
            _mockServer
                .Given(
                    Request.Create()
                        .WithPath("/v1/latest")
                        .UsingGet()
                )
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBody(JsonConvert.SerializeObject(stubResponseObject))
                );
        }

        public void Dispose()
        {
            _mockServer.Reset();
            _mockServer.Stop();
        }
    }
}
