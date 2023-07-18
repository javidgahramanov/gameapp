using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.WebApi.Host.Settings
{
    public static class InfrastructureRegister
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.BuildServiceProvider();
        }
    }
}
