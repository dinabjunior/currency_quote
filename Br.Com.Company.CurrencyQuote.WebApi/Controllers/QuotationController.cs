﻿using System;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Domain.Dtos.Quote;
using Br.Com.Company.CurrencyQuote.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Br.Com.Company.CurrencyQuote.WebApi.Controllers
{
    [ApiController]
    [Route("api/quotation")]
    public class QuotationController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly ILogger _logger;

        public QuotationController(IQuoteService quoteService, ILogger<QuotationController> logger)
        {
            _quoteService = quoteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CalculateQuote([FromQuery] CalculateQuoteRequest request)
        {
            try
            {
                var result = await _quoteService.CalculateQuoteAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro to calculate quote");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while executing the operation. See log to view error message.");
            }
        }
    }
}
