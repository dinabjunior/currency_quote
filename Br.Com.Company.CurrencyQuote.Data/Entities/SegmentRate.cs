using Br.Com.Company.CurrencyQuote.Data.Entities.Base;
using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;

namespace Br.Com.Company.CurrencyQuote.Data.Entities
{
    public class SegmentRate : EntityBase
    {
        public decimal Rate { get; set; }
        public SegmentEnum Segment { get; set; }
    }
}
