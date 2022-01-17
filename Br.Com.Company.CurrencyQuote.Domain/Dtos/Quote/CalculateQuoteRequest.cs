using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;
using FluentValidation;

namespace Br.Com.Company.CurrencyQuote.Domain.Dtos.Quote
{
    public class CalculateQuoteRequest
    {
        public decimal QtdForeignCurrency { get; set; }
        public ForeignCurrencyEnum ForeignCurrency { get; set; }
        public SegmentEnum Segment { get; set; }

        public class Validator : AbstractValidator<CalculateQuoteRequest>
        {
            public Validator()
            {
                RuleFor(x => x.QtdForeignCurrency).NotEmpty().GreaterThan(0).ScalePrecision(4, 18, true);
                RuleFor(x => x.ForeignCurrency).NotEmpty().IsInEnum();
                RuleFor(x => x.Segment).NotEmpty().IsInEnum();
            }
        }
    }
}
