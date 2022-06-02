using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Models;

namespace Soulgram.Interests.Persistence.Migration;

// TODO make it not static
public static class DbMigrator
{
    // TODO refactor method
    public static void SetUpDb(this IMongoClient mongoClient, IConfiguration configuration)
    {
        var dbSettings = configuration
            .GetSection("InterestsDb")
            .Get<InterestsDbSettings>();

        var db = mongoClient.GetDatabase(dbSettings.DatabaseName);

        CreateUniqueIndex<Genre>(db, genre => genre.Name);
        CreateUniqueIndex<UserInterests>(db, ui => ui.UserId);
    }

    private static void CreateUniqueIndex<T>(IMongoDatabase db, Expression<Func<T, object>> field)
    {
        var collection = db.GetCollection<T>(nameof(T));
        var uniqueKeyName = Builders<T>.IndexKeys.Text(field);
        var indexModel = new CreateIndexModel<T>(uniqueKeyName, new CreateIndexOptions {Unique = true});

        collection.Indexes.CreateOneAsync(indexModel)
            .GetAwaiter()
            .GetResult();
    }
}