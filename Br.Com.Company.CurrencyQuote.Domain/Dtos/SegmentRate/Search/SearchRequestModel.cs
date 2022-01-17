using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;
using FluentValidation;

namespace Br.Com.Company.CurrencyQuote.Domain.Dtos
{
    public class SearchRequestModel
    {
        public SegmentEnum? Segment { get; set; }

        public class Validator : AbstractValidator<SearchRequestModel>
        {
            public Validator()
            {
                RuleFor(x => x.Segment).IsInEnum();
            }
        }
    }
}
