using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Br.Com.Company.CurrencyQuote.Data.Persistence.Repository
{
    public interface IRepository : IDisposable
    {
        DbSet<TEntity> Database<TEntity>() where TEntity : EntityBase;

        Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : EntityBase;

        Task InsertAsync<TEntity>(TEntity entity) where TEntity : EntityBase;

        void Delete<TEntity>(TEntity entity) where TEntity : EntityBase;

        void Update<TEntity>(TEntity entity) where TEntity : EntityBase;

        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase;

        Task<TEntity> QueryFirsOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase;

        Task CommitAsync();
    }
}
