using System;
using System.Linq;
using System.Threading.Tasks;
using GameService.DataBase.Contracts.Services.Base;
using GameService.DataContracts.Contracts;
using GameService.Domain.Entities.Base;

namespace GameService.Database.Implementation.Services.Base
{
    public abstract class BaseCrudRepository<TEntity> : BaseQueryRepository<TEntity>, ICrudRepository<TEntity> where TEntity : class, IEntity
    {
        protected BaseCrudRepository(IDbContextFactory dbContextFactory)
            : base(dbContextFactory)
        {

        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await DbSet.AddAsync(entity);

            return result.Entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (DbSet.Local.All(x => x.Id != entity.Id))
            {
                entity = DbSet.Update(entity).Entity;
            }

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid entityId)
        {
            var dbEntity = await GetAsync(entityId);

            DbSet.Remove(dbEntity);

            return true;
        }

        public Task CreateBulkAsync(TEntity[] entities)
        {
            return DbSet.AddRangeAsync(entities);
        }
    }
}