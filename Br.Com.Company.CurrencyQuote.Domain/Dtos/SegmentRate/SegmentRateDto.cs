using System;
using AutoMapper;
using Br.Com.Company.CurrencyQuote.Data.Entities;
using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;

namespace Br.Com.Company.CurrencyQuote.Domain.Dtos
{
    [AutoMap(typeof(SegmentRate))]
    public class SegmentRateDto
    {
        public Guid Id { get; private set; }
        public decimal Rate { get; private set; }
        public SegmentEnum Segment { get; private set; }
    }
}
