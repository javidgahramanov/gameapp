using System;
using System.Threading.Tasks;
using GameService.Domain.Entities.Base;

namespace GameService.DataBase.Contracts.Services.Base
{
    public interface ICrudRepository<TEntity> : IQueryRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(Guid entityId);

        Task CreateBulkAsync(TEntity[] entities);
    }
}