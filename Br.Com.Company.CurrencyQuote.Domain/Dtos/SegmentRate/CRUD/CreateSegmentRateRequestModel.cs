using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;
using FluentValidation;

namespace Br.Com.Company.CurrencyQuote.Domain.Dtos
{
    public class CreateSegmentRateRequestModel
    {
        public decimal Rate { get; set; }
        public SegmentEnum Segment { get; set; }

        public class Validator : AbstractValidator<CreateSegmentRateRequestModel>
        {
            public Validator()
            {
                RuleFor(x => x.Rate).GreaterThanOrEqualTo(0).ScalePrecision(4, 18, true);
                RuleFor(x => x.Segment).NotEmpty().IsInEnum();
            }
        }
    }
}
