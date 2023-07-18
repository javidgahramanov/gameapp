using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameService.Service.Contracts.Services.Base
{
    public interface IQueryService<TEntity> : IService
    {
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null, IEnumerable<string> includes = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> GetAsync(Guid modelId, IEnumerable<string> includes = null);

        Task<TEntity> FindAsync(Guid entityId, IEnumerable<string> includes = null);
    }
}