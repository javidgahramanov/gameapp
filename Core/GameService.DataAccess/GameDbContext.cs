using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameService.DataAccess
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent Configuration

           
        }

        public DbSet<Player> Players { get; set; }
    }
}
