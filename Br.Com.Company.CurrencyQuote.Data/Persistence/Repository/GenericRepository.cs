using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Br.Com.Company.CurrencyQuote.Data.Persistence.Repository
{
    internal partial class GenericRepository<TContext> : IRepository where TContext : DbContext
    {
        private TContext _context;

        public GenericRepository(TContext context)
        {
            _context = context;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        #endregion

        public DbSet<TEntity> Database<TEntity>() where TEntity : EntityBase => _context.Set<TEntity>();
        public async Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase =>
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

        public void Update<TEntity>(TEntity entity) where TEntity : EntityBase =>
            _context.Set<TEntity>().Update(entity);

        public void Delete<TEntity>(TEntity entity) where TEntity : EntityBase =>
            _context.Set<TEntity>().Remove(entity);

        public async Task<TEntity> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default) where TEntity : EntityBase =>
            await _context.Set<TEntity>().Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : EntityBase =>
            await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);

        public async Task<TEntity> QueryFirsOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : EntityBase =>
            await _context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync(cancellationToken);

        public async Task CommitAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken);
    }
}
