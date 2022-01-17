using System;
using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;
using FluentValidation;

namespace Br.Com.Company.CurrencyQuote.Domain.Dtos
{
    public class SearchRateRequestModel
    {
        public Guid? Id { get; set; }
        public SegmentEnum? Segment { get; set; }

        public class Validator : AbstractValidator<SearchRateRequestModel>
        {
            public Validator()
            {
                RuleFor(cliente => cliente).Must(x => (x.Id.HasValue && x.Id.Value != default) || x.Segment.HasValue).WithMessage("Enter at least one filter to perform the operation");
                RuleFor(x => x.Segment).IsInEnum();
            }
        }
    }
}
