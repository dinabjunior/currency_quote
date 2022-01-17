using System;
using MediatR;

namespace Br.Com.Company.CurrencyQuote.Data.Application.Commands
{
    public class DeleteSegmentRateCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
