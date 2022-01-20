using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Application.Commands.Quote;
using Br.Com.Company.CurrencyQuote.Data.Support.Dtos;
using Br.Com.Company.CurrencyQuote.Data.Support.Options;
using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace Br.Com.Company.CurrencyQuote.Data.Application.Handlers
{
    public class ExchangeCommandHandler : IRequestHandler<GetExchangeRateToRealCommand, decimal>
    {
        private static AsyncRetryPolicy _retryPolicy;
        private readonly HttpClient _httpClient;
        private readonly ExchangeRatesApiOptions _options;

        public ExchangeCommandHandler(HttpClient httpClient, IOptions<ExchangeRatesApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<decimal> Handle(GetExchangeRateToRealCommand command, CancellationToken cancellationToken = default)
        {
            _retryPolicy ??= Policy.Handle<FlurlHttpException>()
                                   .WaitAndRetryAsync(new TimeSpan[]
                                   {
                                       TimeSpan.FromSeconds(1),
                                       TimeSpan.FromSeconds(2),
                                       TimeSpan.FromSeconds(3),
                                   });
            var client = new FlurlClient(_httpClient);

            try
            {
                ExchangeRatesApiResponseModel result = null;
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    result = await client.Request(_options.Url, "/v1/latest")
                                   .SetQueryParam("access_key", _options.AccessKey)
                                   .SetQueryParam("symbols", "BRL")
                                   .WithTimeout(TimeSpan.FromSeconds(5))
                                   .GetJsonAsync<ExchangeRatesApiResponseModel>(cancellationToken);
                });

                if (result is null || !result.Success)
                {
                    throw new Exception($"Could not retrieve conversion rate from API {_options.Url}.");
                }

                return result.Rates.BRL;
            }
            catch (FlurlHttpException ex)
            {
                throw new Exception($"Could not retrieve conversion rate from API {_options.Url}", ex);
            }
        }
    }
}
