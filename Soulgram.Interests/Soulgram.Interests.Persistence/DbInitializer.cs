using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Soulgram.Interests.Domain;
using Soulgram.Mongo.Repository;
using Soulgram.Mongo.Repository.Models;

namespace Soulgram.Interests.Persistence;

// TODO make it not static
public static class DbInitializer
{
    // TODO refactor method
    public static void SetUpDb(this IMongoClient mongoClient, IConfiguration configuration)
    {
        var dbSettings = configuration
            .GetSection("DbSettings")
            .Get<DbSettings>();

        var db = mongoClient.GetDatabase(dbSettings?.DatabaseName);

        DbHelper.CreateUniqueIndex<Genre>(db, nameof(Genre), genre => genre.Name);
        DbHelper.CreateUniqueIndex<Interest>(db, nameof(Interest), ui => ui.Type);
        //CreateUniqueIndex<UserFavorites>(db, nameof(UserFavorites), ui => ui.UserId);
    }
}