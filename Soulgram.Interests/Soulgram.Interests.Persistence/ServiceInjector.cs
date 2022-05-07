using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Soulgram.Interests.Application;
using Soulgram.Interests.Domain;

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

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
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

            cm.SetIgnoreExtraElements(true);
        });
    }

    public static void SetUpDb(this IMongoClient mongoClient, IConfiguration configuration)
    {
        var dbSettings = GetInterestsDbSettings(configuration);

        var db = mongoClient.GetDatabase(dbSettings.DatabaseName);
        var genreCollection = db.GetCollection<Genre>(nameof(Genre));

        var indexKeysDefinition = Builders<Genre>.IndexKeys.Text(genre => genre.Name);

        var indexModel = new CreateIndexModel<Genre>(indexKeysDefinition,
            new CreateIndexOptions
            {
                Unique = true
            });

        genreCollection.Indexes.CreateOneAsync(indexModel).GetAwaiter().GetResult();
    }
}