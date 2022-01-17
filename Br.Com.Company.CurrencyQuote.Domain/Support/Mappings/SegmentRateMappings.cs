using AutoMapper;
using Br.Com.Company.CurrencyQuote.Data.Application.Commands;
using Br.Com.Company.CurrencyQuote.Domain.Dtos;

namespace Br.Com.Company.CurrencyQuote.Domain.Support.Mappings
{
    public class SegmentRateMappings : Profile
    {
        public SegmentRateMappings()
        {
            CreateMap<CreateSegmentRateRequestModel, CreateSegmentRateCommand>();
            CreateMap<UpdateSegmentRateRequestModel, UpdateSegmentRateCommand>();
        }
    }
}
