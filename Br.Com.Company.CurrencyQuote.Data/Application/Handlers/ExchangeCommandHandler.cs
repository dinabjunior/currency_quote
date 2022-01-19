using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Application.Commands.Quote;
using Br.Com.Company.CurrencyQuote.Data.Support.Dtos;
using Br.Com.Company.CurrencyQuote.Data.Support.Options;
using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Options;

namespace Br.Com.Company.CurrencyQuote.Data.Application.Handlers
{
    public class ExchangeCommandHandler : IRequestHandler<GetExchangeRateToRealCommand, decimal>
    {
        private readonly HttpClient _httpClient;
        private readonly ExchangeRatesApiOptions _options;

        public ExchangeCommandHandler(HttpClient httpClient, IOptions<ExchangeRatesApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<decimal> Handle(GetExchangeRateToRealCommand command, CancellationToken cancellationToken = default)
        {
            var client = new FlurlClient(_httpClient);
            var result = await client.Request(_options.Url, "/v1/latest")
                               .SetQueryParam("access_key", _options.AccessKey)
                               .SetQueryParam("symbols", "BRL")
                               .GetJsonAsync<ExchangeRatesApiResponseModel>(cancellationToken);

            if (result?.Success == true)
            {
                return result.Rates.BRL;
            }

            return 0M;
        }
    }
}
