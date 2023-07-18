using GameService.DataAccess.Services;
using GameService.DataContracts.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.DataAccess
{
    public static class ContainerExtensions
    {
        public static IServiceCollection UseUowServices(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            service.AddScoped<IDbContextFactory, DbContextFactory>();


            return service;
        }
    }
}
