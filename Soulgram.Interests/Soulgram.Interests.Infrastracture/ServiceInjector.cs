using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Interests.Application.Interfaces;

namespace Soulgram.Interests.Infrastracture;

public static class ServiceInjector
{
    public static void AddInfrastructure(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddOttService(configuration);
    }

    private static void AddOttService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<OttClientSettings>(options =>
            configuration.GetSection("OTTClientSettings").Bind(options));

        serviceCollection.AddHttpClient<OttClient>();
        serviceCollection.AddScoped<IMovieService, OttClient>();
    }
}