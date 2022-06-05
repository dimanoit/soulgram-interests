using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Infrastructure.Clients;
using Soulgram.Interests.Infrastructure.Clients.Implementation;
using Soulgram.Interests.Infrastructure.Facades;
using Soulgram.Interests.Infrastructure.Filters;
using Soulgram.Interests.Infrastructure.Models.HttpClientParams;
using Soulgram.Interests.Infrastructure.Services;

namespace Soulgram.Interests.Infrastructure;

public static class ServiceInjector
{
    public static void AddInfrastructure(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddHttpClients(configuration);

        serviceCollection.AddTransient<IMovieResponseFilter, MovieResponseFilter>();
        serviceCollection.AddScoped<IMovieFacade, MovieFacade>();
        serviceCollection.AddScoped<IMovieService, MovieService>();
    }

    private static void AddHttpClients(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<OttClientSettings>(options =>
            configuration.GetSection("OTTClientSettings").Bind(options));

        serviceCollection.Configure<MovieDatabaseClientSettings>(options =>
            configuration.GetSection("MovieDatabaseClientSettings").Bind(options));

        serviceCollection.AddHttpClient<IReserveMovieClient, ReserveMovieClient>();
        serviceCollection.AddHttpClient<IMovieDatabaseClient, MainMovieClient>();
    }
}