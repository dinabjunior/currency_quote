using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;
using MediatR;

namespace Br.Com.Company.CurrencyQuote.Data.Application.Commands.Quote
{
    public class GetExchangeRateToRealCommand : IRequest<decimal>
    {
        public ForeignCurrencyEnum ForeignCurrency { get; set; }
    }
}
