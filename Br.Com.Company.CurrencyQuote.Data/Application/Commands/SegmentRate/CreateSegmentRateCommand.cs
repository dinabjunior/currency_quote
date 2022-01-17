using System;
using Br.Com.Company.CurrencyQuote.Data.Entities;
using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;
using MediatR;

namespace Br.Com.Company.CurrencyQuote.Data.Application.Commands
{
    public class CreateSegmentRateCommand : IRequest<Guid>
    {
        public decimal Rate { get; set; }
        public SegmentEnum Segment { get; set; }

        public static explicit operator SegmentRate(CreateSegmentRateCommand command) =>
           new SegmentRate
           {
               Id = Guid.NewGuid(),
               Rate = command.Rate,
               Segment = command.Segment
           };
    }
}
