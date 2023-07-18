using GameService.Core.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.WebApi.Host.Settings
{
    public static class OptionRegister
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration.GetSection<ConnectionStrings>());
        }

        private static IConfigurationSection GetSection<T>(this IConfiguration configuration)
        {
            return configuration.GetSection(GetSectionName<T>());
        }

        private static string GetSectionName<T>()
        {
            return typeof(T).Name;
        }
    }
}
