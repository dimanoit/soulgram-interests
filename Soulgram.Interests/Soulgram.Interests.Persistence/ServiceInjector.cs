using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.DataAccessors;
using Soulgram.Interests.Persistence.Interfaces;
using Soulgram.Interests.Persistence.Models;

namespace Soulgram.Interests.Persistence;

public static class ServiceInjector
{
    public static void AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<InterestsDbSettings>(configuration.GetSection("InterestsDb"));
        var interestsDbSettings = GetInterestsDbSettings(configuration);

        ConfigureMongoModels();

        services.AddSingleton<IMongoClient, MongoClient>(sp
            => new MongoClient(interestsDbSettings.ConnectionString));

        services.AddScoped(typeof(IMongoCollectionProvider<>), typeof(MongoCollectionProvider<>));
        services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserInterestsRepository, UserInterestsRepository>();
    }

    private static InterestsDbSettings GetInterestsDbSettings(IConfiguration configuration)
    {
        var interestsDbSettings = configuration
                                  .GetSection("InterestsDb")
                                  .Get<InterestsDbSettings>();
        return interestsDbSettings;
    }

    private static void ConfigureMongoModels()
    {
        BsonClassMap.RegisterClassMap<Genre>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(genre => genre.Id)
              .SetIdGenerator(new StringObjectIdGenerator())
              .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(genre => genre.Name).SetIsRequired(true);
            cm.MapMember(genre => genre.UsersIds).SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
        
        BsonClassMap.RegisterClassMap<UserInterests>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(ui => ui.Id)
                .SetIdGenerator(new StringObjectIdGenerator())
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(ui => ui.UserId).SetIsRequired(true);
            cm.MapMember(ui => ui.Interests).SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }

    public static void SetUpDb(this IMongoClient mongoClient, IConfiguration configuration)
    {
        var dbSettings = GetInterestsDbSettings(configuration);

        var db = mongoClient.GetDatabase(dbSettings.DatabaseName);
        var genreCollection = db.GetCollection<Genre>(nameof(Genre));
        var userInterestsCollection = db.GetCollection<UserInterests>(nameof(UserInterests));

        var genreUniqueNameKey = Builders<Genre>.IndexKeys.Text(genre => genre.Name);
        var userInterestsUserIdKey = Builders<UserInterests>.IndexKeys.Text(ui => ui.UserId);

        var createGenreKeyModel = new CreateIndexModel<Genre>(genreUniqueNameKey,
            new CreateIndexOptions
            {
                Unique = true,
                
            });
        
        var userInterestsUserIdKeyModel = new CreateIndexModel<UserInterests>(userInterestsUserIdKey,
            new CreateIndexOptions
            {
                Unique = true,
            });

        genreCollection.Indexes.CreateOneAsync(createGenreKeyModel).GetAwaiter().GetResult();
        userInterestsCollection.Indexes.CreateOneAsync(userInterestsUserIdKeyModel).GetAwaiter().GetResult();
    }
}