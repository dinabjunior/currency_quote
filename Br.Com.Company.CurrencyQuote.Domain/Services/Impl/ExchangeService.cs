using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Application.Commands.Quote;
using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;
using MediatR;

namespace Br.Com.Company.CurrencyQuote.Domain.Services.Impl
{
    internal class ExchangeService : IExchangeService
    {
        private readonly IMediator _mediator;

        public ExchangeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<decimal> GetExchangeRateToReal(ForeignCurrencyEnum currency)
        {
            return await _mediator.Send(new GetExchangeRateToRealCommand { ForeignCurrency = currency }).ConfigureAwait(false);
        }
    }
}
