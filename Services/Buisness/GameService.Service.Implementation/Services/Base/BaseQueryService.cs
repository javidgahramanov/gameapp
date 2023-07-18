using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameService.DataBase.Contracts.Services.Base;
using GameService.Domain.Entities.Base;
using GameService.Service.Contracts.Services.Base;

namespace GameService.Service.Implementation.Services.Base
{
    public abstract class BaseQueryService<TEntity, TRepository> : BaseService, IQueryService<TEntity>
        where TEntity : class, IEntity
        where TRepository : ICrudRepository<TEntity>
    {
        protected TRepository Repository { get; }

        protected BaseQueryService(TRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null, IEnumerable<string> includes = null)
        {
            return await Repository.QueryAsync(filter, includes);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await Repository.QueryCountAsync(filter);
        }

        public virtual async Task<TEntity> GetAsync(Guid modelId, IEnumerable<string> includes = null)
        {
            return await Repository.GetAsync(modelId, includes);
        }

        public async Task<TEntity> FindAsync(Guid entityId, IEnumerable<string> includes = null)
        {
            return await Repository.FindAsync(entityId, includes);
        }
    }
}