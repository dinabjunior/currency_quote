using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Domain.Dtos;
using Br.Com.Company.CurrencyQuote.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Br.Com.Company.CurrencyQuote.WebApi.Controllers
{
    [ApiController]
    [Route("api/quotation")]
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
        public async Task<IActionResult> CreateSegmenteRate([FromBody] CreateSegmentRateRequestModel model)
        {
            var id = await SegmentRateService.CreateSegmentRateAsync(model).ConfigureAwait(false);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSegmentRate([FromBody] UpdateSegmentRateRequestModel model)
        {
            await SegmentRateService.UpdateSegmentRateAsync(model).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSegmentRate([RequireNonDefault] Guid id)
        {
            await SegmentRateService.DeleteSegmentRateByIdAsync(id).ConfigureAwait(false);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SegmentRateDto>> GetSegmentRate([RequireNonDefault] Guid id)
        {
            var segmentRate = await SegmentRateService.GetSegmentRateByIdAsync(id).ConfigureAwait(false);
            if (segmentRate == null)
            {
                return NotFound();
            }

            return segmentRate;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchRequestModel model)
        {
            var result = await SegmentRateService.SearchAsync(model).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("searchRate")]
        public async Task<ActionResult<decimal>> GetRateBySegment([FromQuery] SearchRateRequestModel model)
        {
            var result = await SegmentRateService.GetRateBySegmentAsync(model).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound();
            }

            return result.Rate;
        }
    }
}
