using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Domain.Dtos;
using Br.Com.Company.CurrencyQuote.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Br.Com.Company.CurrencyQuote.WebApi.Controllers
{
    [ApiController]
    [Route("api/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IServiceProvider _services;
        private ISegmentRateService _segmentRateService;

        public ConfigurationController(IServiceProvider services)
        {
            _services = services;
        }

        private ISegmentRateService SegmentRateService => _segmentRateService ??= _services.GetService<ISegmentRateService>();

        [HttpPost]
        public async Task<IActionResult> CreateSegmenteRate([FromBody] CreateSegmentRateRequestModel model, CancellationToken cancellationToken)
        {
            var id = await SegmentRateService.CreateSegmentRateAsync(model, cancellationToken).ConfigureAwait(false);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSegmentRate([FromBody] UpdateSegmentRateRequestModel model, CancellationToken cancellationToken)
        {
            await SegmentRateService.UpdateSegmentRateAsync(model, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSegmentRate([RequireNonDefault] Guid id, CancellationToken cancellationToken)
        {
            await SegmentRateService.DeleteSegmentRateByIdAsync(id, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SegmentRateDto>> GetSegmentRate([RequireNonDefault] Guid id, CancellationToken cancellationToken)
        {
            var segmentRate = await SegmentRateService.GetSegmentRateByIdAsync(id, cancellationToken).ConfigureAwait(false);
            if (segmentRate == null)
            {
                return NotFound();
            }

            return segmentRate;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchRequestModel model, CancellationToken cancellationToken)
        {
            var result = await SegmentRateService.SearchAsync(model, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("searchRate")]
        public async Task<ActionResult<decimal>> GetRateBySegment([FromQuery] SearchRateRequestModel model, CancellationToken cancellationToken)
        {
            var result = await SegmentRateService.GetRateBySegmentAsync(model, cancellationToken).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound();
            }

            return result.Rate;
        }
    }
}
