using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Models;

namespace Soulgram.Interests.Persistence.Migration;

// TODO make it not static
public static class DbInitializer
{
    // TODO refactor method
    public static void SetUpDb(this IMongoClient mongoClient, IConfiguration configuration)
    {
        var dbSettings = configuration
            .GetSection("InterestsDb")
            .Get<InterestsDbSettings>();

        var db = mongoClient.GetDatabase(dbSettings.DatabaseName);

        CreateUniqueIndex<Genre>(db, nameof(Genre), genre => genre.Name);
        CreateUniqueIndex<Interest>(db, nameof(Interest), ui => ui.Type);
        //CreateUniqueIndex<UserFavorites>(db, nameof(UserFavorites), ui => ui.UserId);
    }

    private static void CreateUniqueIndex<T>(
        IMongoDatabase db,
        string collectionName,
        Expression<Func<T, object>> field)
    {
        var collection = db.GetCollection<T>(collectionName);
        var uniqueKeyName = Builders<T>.IndexKeys.Ascending(field);
        var indexModel = new CreateIndexModel<T>(uniqueKeyName, new CreateIndexOptions
        {
            Unique = true
        });

        collection.Indexes.CreateOneAsync(indexModel)
            .GetAwaiter()
            .GetResult();
    }
}