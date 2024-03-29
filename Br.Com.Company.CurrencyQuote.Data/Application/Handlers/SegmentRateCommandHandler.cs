﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications;
using Br.Com.Company.CurrencyQuote.Data.Application.Commands;
using Br.Com.Company.CurrencyQuote.Data.Entities;
using Br.Com.Company.CurrencyQuote.Data.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Br.Com.Company.CurrencyQuote.Data.Application.Handlers
{
    public class SegmentRateCommandHandler :
        AsyncRequestHandler<UpdateSegmentRateCommand>,
        IRequestHandler<CreateSegmentRateCommand, Guid>,
        IRequestHandler<DeleteSegmentRateCommand>
    {
        private readonly IRepository _repository;
        private readonly INotify _notifications;

        public SegmentRateCommandHandler(IRepository repository, INotify notifications)
        {
            _repository = repository;
            _notifications = notifications;
        }

        public async Task<Guid> Handle(CreateSegmentRateCommand command, CancellationToken cancellationToken = default)
        {
            var segmentRate = (SegmentRate)command;

            var segmentExists = await _repository.Database<SegmentRate>().AnyAsync(e => e.Segment == segmentRate.Segment, cancellationToken);
            if (segmentExists)
            {
                _notifications.AddNotification(nameof(segmentRate.Segment), $"Segment '{segmentRate.Segment}' already exists.");
                return Guid.Empty;
            }

            await _repository.InsertAsync(segmentRate, cancellationToken);
            await _repository.CommitAsync(cancellationToken);

            return segmentRate.Id;
        }

        protected override async Task Handle(UpdateSegmentRateCommand command, CancellationToken cancellationToken = default)
        {
            var segmentRate = await _repository.GetByIdAsync<SegmentRate>(command.Id, cancellationToken);
            command.CopyValues(segmentRate);

            if (segmentRate == null)
            {
                _notifications.AddNotification(nameof(command.Id), $"No segment found with id '{command.Id}'.");
                return;
            }

            _repository.Update(segmentRate);
            await _repository.CommitAsync(cancellationToken);
        }

        public async Task<Unit> Handle(DeleteSegmentRateCommand command, CancellationToken cancellationToken = default)
        {
            var segmentRate = await _repository.Database<SegmentRate>()
                                            .Where(e => e.Id == command.Id)
                                            .Select(e => new SegmentRate { Id = e.Id })
                                            .FirstOrDefaultAsync(cancellationToken);

            if (segmentRate == null)
            {
                _notifications.AddNotification(nameof(command.Id), $"No segment found with id '{command.Id}'.");
                return Unit.Value;
            }

            _repository.Database<SegmentRate>().Remove(segmentRate);
            await _repository.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
