using GameService.Database.Implementation;
using GameService.Service.Contracts.Services;
using GameService.Service.Implementation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Service.Implementation
{
    public static class ContainerExtensions
    {
        public static IServiceCollection UseServices(this IServiceCollection service)
        {
            service.AddScoped<IPlayerService, PlayerService>();
            service.UseDatabase();

            return service;
        }
    }
}
