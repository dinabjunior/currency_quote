using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Domain.Dtos.Quote;

namespace Br.Com.Company.CurrencyQuote.Domain.Services
{
    public interface IQuoteService
    {
        Task<decimal> CalculateQuoteAsync(CalculateQuoteRequest request);
    }
}
