using GameService.DataContracts.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameService.DataAccess.Services
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly GameDbContext _dbContext;

        public DbContextFactory(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContext GetContext()
        {
            return _dbContext;
        }
    }
}
