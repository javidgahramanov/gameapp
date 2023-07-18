using System;
using Microsoft.Extensions.Configuration;

namespace GameService.WebApi.Host
{
    public class AppConfiguration
    {
        public static IConfiguration Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if DEBUG
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true, reloadOnChange: true)
#endif
                .Build();

            return configuration;
        }
    }
}
