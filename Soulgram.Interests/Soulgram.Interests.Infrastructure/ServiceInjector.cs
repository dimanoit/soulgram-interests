using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Infrastructure.Clients;
using Soulgram.Interests.Infrastructure.Filters;

namespace Soulgram.Interests.Infrastructure;

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

        serviceCollection.AddTransient<IMovieResponseFilter, MovieResponseFilter>();
        serviceCollection.AddHttpClient<OttClient>();
        serviceCollection.AddScoped<IMovieService, OttClient>();
    }
}