using GameService.DataBase.Contracts.Services;
using GameService.Database.Implementation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Database.Implementation
{
    public static class ContainerExtensions
    {
        public static IServiceCollection UseDatabase(this IServiceCollection service)
        {
            service.AddScoped<IPlayerRepository, PlayerRepository>();
            return service;
        }
    }
}
