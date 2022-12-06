using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Persistence.DataAccessors;
using Soulgram.Mongo.Repository;
using Soulgram.Mongo.Repository.Interfaces;
using Soulgram.Mongo.Repository.Models;

namespace Soulgram.Interests.Persistence;

public static class ServiceInjector
{
    public static void AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DbSettings>(configuration.GetSection("DbSettings"));
        var interestsDbSettings = configuration
            .GetSection("DbSettings")
            .Get<DbSettings>();

        new MongoMapper().MapModels();

        services.AddSingleton<IMongoClient, MongoClient>(sp
            => new MongoClient(interestsDbSettings?.ConnectionString));

        services.AddScoped<IMongoConnection, MongoConnection>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IInterestsRepository, InterestsRepository>();
        services.AddScoped<IUserFavoritesRepository, UserFavoritesRepository>();
    }
}