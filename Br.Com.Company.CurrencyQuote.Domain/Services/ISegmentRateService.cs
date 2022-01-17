using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Domain.Dtos;

namespace Br.Com.Company.CurrencyQuote.Domain.Services
{
    public interface ISegmentRateService
    {
        #region CRUD Methods
        Task<Guid> CreateSegmentRateAsync(CreateSegmentRateRequestModel model);
        Task UpdateSegmentRateAsync(UpdateSegmentRateRequestModel model);
        Task DeleteSegmentRateByIdAsync(Guid id);
        Task<SegmentRateDto> GetSegmentRateByIdAsync(Guid id);
        #endregion

        Task<IEnumerable<SegmentRateDto>> SearchAsync(SearchRequestModel model);
        Task<RateDto> GetRateBySegmentAsync(SearchRateRequestModel model);
    }
}
