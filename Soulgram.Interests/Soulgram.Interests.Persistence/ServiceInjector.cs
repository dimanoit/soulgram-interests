using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Persistence.DataAccessors;
using Soulgram.Interests.Persistence.DataAccessors.Concrete;
using Soulgram.Interests.Persistence.Interfaces;
using Soulgram.Interests.Persistence.Migration.ModelMappers;
using Soulgram.Interests.Persistence.Models;

namespace Soulgram.Interests.Persistence;

public static class ServiceInjector
{
    public static void AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<InterestsDbSettings>(configuration.GetSection("InterestsDb"));
        var interestsDbSettings = configuration
            .GetSection("InterestsDb")
            .Get<InterestsDbSettings>();

        new MongoMapper().MapModels();

        services.AddSingleton<IMongoClient, MongoClient>(sp
            => new MongoClient(interestsDbSettings.ConnectionString));

        services.AddScoped<IMongoConnection, MongoConnection>();
        services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IInterestsRepository, InterestsRepository>();
        services.AddScoped<IInterestsIdsRepository, InterestsIdsRepository>();
        services.AddScoped<IUserFavoritesRepository, UserFavoritesRepository>();
    }
}