using System;
using FluentValidation;

namespace Br.Com.Company.CurrencyQuote.Domain.Dtos
{
    public class UpdateSegmentRateRequestModel
    {
        public Guid Id { get; set; }
        public decimal Rate { get; set; }

        public class Validator : AbstractValidator<UpdateSegmentRateRequestModel>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Rate).GreaterThanOrEqualTo(0).ScalePrecision(4, 18, true);
            }
        }
    }
}
