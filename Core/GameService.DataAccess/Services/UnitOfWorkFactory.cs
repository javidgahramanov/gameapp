using GameService.DataContracts.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameService.DataAccess.Services
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory _dbContextFactory;

        public UnitOfWorkFactory(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
           
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_dbContextFactory);
        }

        public void Rollback()
        {
            var dbContext = _dbContextFactory.GetContext();

            foreach (var dbEntityEntry in dbContext.ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }
    }
}