using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
        var interestsDbSettings = configuration
            .GetSection("InterestsDb")
            .Get<InterestsDbSettings>();

        ConfigureMongoModels();

        services.AddSingleton<IMongoClient, MongoClient>(sp
            => new MongoClient(interestsDbSettings.ConnectionString));

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }

    private static void ConfigureMongoModels()
    {
        BsonClassMap.RegisterClassMap<Genre>(classMap =>
        {
            classMap
                .MapIdMember(g => g.Name)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            classMap
                .MapProperty(g => g.UsersIds)
                .SetSerializer(new ArraySerializer<string>(new StringSerializer(BsonType.ObjectId)));
        });
    }
}