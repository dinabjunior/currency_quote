using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Domain.Dtos;

namespace Br.Com.Company.CurrencyQuote.Domain.Services
{
    public interface ISegmentRateService
    {
        #region CRUD Methods
        Task<Guid> CreateSegmentRateAsync(CreateSegmentRateRequestModel model, CancellationToken cancellationToken = default);
        Task UpdateSegmentRateAsync(UpdateSegmentRateRequestModel model, CancellationToken cancellationToken = default);
        Task DeleteSegmentRateByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SegmentRateDto> GetSegmentRateByIdAsync(Guid id, CancellationToken cancellationToken = default);
        #endregion

        Task<IEnumerable<SegmentRateDto>> SearchAsync(SearchRequestModel model, CancellationToken cancellationToken = default);
        Task<RateDto> GetRateBySegmentAsync(SearchRateRequestModel model, CancellationToken cancellationToken = default);
    }
}
