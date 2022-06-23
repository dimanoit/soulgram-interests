using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Services;
using Soulgram.Logging;
using Soulgram.Logging.Models;

namespace Soulgram.Interests.Application;

public static class ServiceInjector
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services
            .AddMediatR(currentAssembly)
            .AddLogging(configuration);

        services.AddScoped<IUserFavoritesService, UserFavoritesService>();
    }


    private static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var loggingSettings = configuration
            .GetSection("LoggingSettings")
            .Get<LoggingSettings>();

        return services.AddLogging(loggingSettings);
    }
}