using System;
using System.Threading.Tasks;

namespace GameService.Service.Contracts.Services.Base
{
    public interface ICrudService<TEntity> : IQueryService<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity model);

        Task<TEntity> UpdateAsync(TEntity model);

        Task<bool> DeleteAsync(Guid id);
    }
}