using System;
using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;

namespace Br.Com.Company.CurrentQuote.IT.Models
{
    public class SegmentRateModel
    {
        public Guid Id { get; set; }
        public decimal Rate { get; set; }
        public SegmentEnum Segment { get; set; }
    }
}
