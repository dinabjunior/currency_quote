using System;
using Br.Com.Company.CurrencyQuote.Data.Entities;
using MediatR;

namespace Br.Com.Company.CurrencyQuote.Data.Application.Commands
{
    public class UpdateSegmentRateCommand : IRequest
    {
        public Guid Id { get; set; }
        public decimal Rate { get; set; }

        public void CopyValues(SegmentRate segmentRate)
        {
            if (segmentRate != null)
            {
                segmentRate.Rate = Rate;
            }
        }
    }
}
