using System.Linq;
using GameService.DataAccess;
using GameService.DataContracts.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameService.Database.Implementation.Services.Base
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected IDbContextFactory DbContextFactory;

        protected GameDbContext Context => (GameDbContext)DbContextFactory.GetContext();

        protected BaseRepository(IDbContextFactory dbContextFactory)
        {
            DbContextFactory = dbContextFactory;
        }

        protected virtual DbSet<TEntity> DbSet => Context.Set<TEntity>();

        protected virtual DbSet<T> GetDbSet<T>() where T : class
        {
            return Context.Set<T>();
        }

        protected virtual IQueryable<TEntity> DbSetAsNoTracking => Context.Set<TEntity>().AsNoTracking();
    }
}
