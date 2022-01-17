using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Br.Com.Company.CurrencyQuote.Data.Application.Commands;
using Br.Com.Company.CurrencyQuote.Data.Entities;
using Br.Com.Company.CurrencyQuote.Data.Persistence.Repository;
using Br.Com.Company.CurrencyQuote.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Br.Com.Company.CurrencyQuote.Domain.Services.Impl
{
    internal class SegmentRateService : ISegmentRateService
    {
        private readonly IServiceProvider _services;
        private IMapper _mapper;
        private IMediator _mediator;
        private IRepository _repository;

        public SegmentRateService(IServiceProvider services)
        {
            _services = services;
        }

        private IMapper Mapper => _mapper ??= _services.GetService<IMapper>();
        private IMediator Mediator => _mediator ??= _services.GetService<IMediator>();
        private IRepository Repository => _repository ??= _services.GetService<IRepository>();

        #region CRUD Methods

        public async Task<Guid> CreateSegmentRateAsync(CreateSegmentRateRequestModel model)
        {
            var command = Mapper.Map<CreateSegmentRateCommand>(model);
            return await Mediator.Send(command);
        }

        public async Task UpdateSegmentRateAsync(UpdateSegmentRateRequestModel model)
        {
            var command = Mapper.Map<UpdateSegmentRateCommand>(model);
            await Mediator.Send(command);
        }

        public async Task DeleteSegmentRateByIdAsync(Guid id) => await Mediator.Send(new DeleteSegmentRateCommand { Id = id });

        public async Task<SegmentRateDto> GetSegmentRateByIdAsync(Guid id)
        {
            return await Repository.Database<SegmentRate>()
                                    .Where(e => e.Id == id)
                                    .ProjectTo<SegmentRateDto>(Mapper.ConfigurationProvider)
                                    .FirstOrDefaultAsync();
        }

        #endregion

        public async Task<IEnumerable<SegmentRateDto>> SearchAsync(SearchRequestModel model)
        {
            var query = Repository.Database<SegmentRate>().AsQueryable();
            if (model.Segment.HasValue)
            {
                query = query.Where(e => e.Segment == model.Segment);
            }

            return await query.ProjectTo<SegmentRateDto>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<RateDto> GetRateBySegmentAsync(SearchRateRequestModel model)
        {
            var query = Repository.Database<SegmentRate>().AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(e => e.Id == model.Id);
            }

            if (model.Segment.HasValue)
            {
                query = query.Where(e => e.Segment == model.Segment);
            }

            return await query.ProjectTo<RateDto>(Mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }
    }
}
