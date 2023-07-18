using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameService.Domain.Entities.Base;

namespace GameService.DataBase.Contracts.Services.Base
{
    public interface IQueryRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity[]> QueryAsync(Expression<Func<TEntity, bool>> filter = null, IEnumerable<string> includes = null);

        Task<int> QueryCountAsync(Expression<Func<TEntity, bool>> filter = null);

        ValueTask<TEntity> GetAsync(Guid entityId, IEnumerable<string> includes = null);

        Task<TEntity> FindAsync(Guid entityId, IEnumerable<string> includes = null);
    }
}
