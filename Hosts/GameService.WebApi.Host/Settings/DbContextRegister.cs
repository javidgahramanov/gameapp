using GameService.Core.Configs;
using GameService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GameService.WebApi.Host.Settings
{
    public static class DbContextRegister
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var dbConnection = serviceProvider.GetRequiredService<IOptions<ConnectionStrings>>();

            services.AddDbContext<GameDbContext>(options => options.UseLazyLoadingProxies().UseSqlite(dbConnection.Value.GameDb));

            services.UseUowServices();

            return services;
        }
    }
}
