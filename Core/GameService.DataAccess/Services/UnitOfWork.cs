using GameService.DataContracts.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameService.DataAccess.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContextManager;

        public UnitOfWork(IDbContextFactory dbContextFactory)
        {
            _dbContextManager = dbContextFactory.GetContext();
        }

        public int Commit()
        {
            return _dbContextManager.ChangeTracker.HasChanges() ? _dbContextManager.SaveChanges() : 0;
        }

        public async Task<int> CommitAsync()
        {
            if (_dbContextManager.ChangeTracker.HasChanges())
            {
                return await _dbContextManager.SaveChangesAsync();
            }

            return 0;
        }

        ~UnitOfWork()
        {
            Dispose();
        }

        public void Dispose()
        {

        }
    }
}