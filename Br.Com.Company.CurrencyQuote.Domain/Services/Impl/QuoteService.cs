﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Entities;
using Br.Com.Company.CurrencyQuote.Data.Persistence.Repository;
using Br.Com.Company.CurrencyQuote.Domain.Dtos.Quote;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Br.Com.Company.CurrencyQuote.Domain.Services.Impl
{
    internal class QuoteService : IQuoteService
    {
        private readonly IServiceProvider _services;
        private IExchangeService _exchangeService;
        private IRepository _repository;

        public QuoteService(IServiceProvider services)
        {
            _services = services;
        }

        private IRepository Repository => _repository ??= _services.GetService<IRepository>();
        private IExchangeService ExchangeService => _exchangeService ??= _services.GetService<IExchangeService>();

        public async Task<decimal> CalculateQuoteAsync(CalculateQuoteRequest request, CancellationToken cancellationToken)
        {
            var conversionRateValue = await ExchangeService.GetExchangeRateToReal(request.ForeignCurrency, cancellationToken);

            var segmentRate = await Repository.Database<SegmentRate>()
                                              .Where(e => e.Segment == request.Segment)
                                              .Select(e => e.Rate)
                                              .FirstOrDefaultAsync(cancellationToken);

            return CalculateExchangeCurrencyToReal(request.QtdForeignCurrency, conversionRateValue, segmentRate);
        }

        private decimal CalculateExchangeCurrencyToReal(decimal qtdForeignCurrency, decimal conversionRateValue, decimal segmentRateValue)
        {
            return (qtdForeignCurrency * conversionRateValue) * (1 + (segmentRateValue / 100));
        }
    }
}
