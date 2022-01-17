using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Application.Commands.Quote;
using Br.Com.Company.CurrencyQuote.Data.Support.Dtos;
using Flurl.Http;
using MediatR;

namespace Br.Com.Company.CurrencyQuote.Data.Application.Handlers
{
    public class ExchangeCommandHandler : IRequestHandler<GetExchangeRateToRealCommand, decimal>
    {
        private readonly HttpClient _httpClient;

        public ExchangeCommandHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> Handle(GetExchangeRateToRealCommand command, CancellationToken cancellationToken)
        {
            var client = new FlurlClient(_httpClient);
            var result = await client.Request("http://api.exchangeratesapi.io/v1/latest")
                               .SetQueryParam("access_key", "0fafb3e3df277f1a4cde89f1727d1c8f")
                               .SetQueryParam("symbols", "BRL")
                               .GetJsonAsync<ExchangeRatesApiResponseModel>();

            if (result?.Success == true)
            {
                return result.Rates.BRL;
            }

            return 0M;
        }
    }
}
