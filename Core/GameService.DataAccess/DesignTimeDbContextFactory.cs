using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GameService.DataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GameDbContext>
    {
        public GameDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GameDbContext>();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#if DEBUG
            configuration.AddJsonFile($"appsettings.{environment}.json", optional: true);
#endif
            var config = configuration.Build();

            builder.UseSqlite(config["ConnectionStrings:GameDb"]);
            builder.EnableSensitiveDataLogging();

            return new GameDbContext(builder.Options);
        }
    }
}