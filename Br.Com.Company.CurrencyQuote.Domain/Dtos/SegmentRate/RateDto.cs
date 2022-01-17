using AutoMapper;
using Br.Com.Company.CurrencyQuote.Data.Entities;

namespace Br.Com.Company.CurrencyQuote.Domain.Dtos
{
    [AutoMap(typeof(SegmentRate))]
    public class RateDto
    {
        public decimal Rate { get; private set; }
    }
}
