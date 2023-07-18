using GameService.DataAccess;
using GameService.Service.Implementation;
using GameService.WebApi.Endpoints.Hubs;
using GameService.WebApi.Host.Logging;
using GameService.WebApi.Host.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using ContainerExtensions = GameService.Database.Implementation.ContainerExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Configure services
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.ConfigureDbContext();
builder.Services.UseServices();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

builder.Services.AddCors();

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
}).AddNewtonsoftJsonProtocol(opt =>
{
    //opt.PayloadSerializerOptions.ReferenceHandler = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddAuthorization();
builder.Services.AddMvcCore()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    })
    .AddApiExplorer()
    .AddApplicationPart(typeof(ContainerExtensions).GetTypeInfo().Assembly);

builder.Services.AddRouting(options => options.LowercaseUrls = true);



var app = builder.Build();
//Configure db migration
using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<GameDbContext>();
context?.Database.Migrate();

// Configure app and routes
app.UseRouting();

app.UseCors("AllowSpecificOrigin");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Sink(new GameHubLog(app.Services.GetRequiredService<IHubContext<GameHub>>()))
    .CreateLogger();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<GameHub>("/api/v1/game");
});

// Run the app
app.Run();