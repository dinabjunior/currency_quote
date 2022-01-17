using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;

namespace Br.Com.Company.CurrencyQuote.Domain.Services
{
    public interface IExchangeService
    {
        Task<decimal> GetExchangeRateToReal(ForeignCurrencyEnum currency);
    }
}
