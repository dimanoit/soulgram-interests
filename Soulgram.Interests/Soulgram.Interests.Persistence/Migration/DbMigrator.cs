using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Models;

namespace Soulgram.Interests.Persistence.Migration;

public static class DbMigrator
{
    // TODO refactor method
    public static void SetUpDb(this IMongoClient mongoClient, IConfiguration configuration)
    {
        var dbSettings = configuration
            .GetSection("InterestsDb")
            .Get<InterestsDbSettings>();

        var db = mongoClient.GetDatabase(dbSettings.DatabaseName);
        var genreCollection = db.GetCollection<Genre>(nameof(Genre));
        var userInterestsCollection = db.GetCollection<UserInterests>(nameof(UserInterests));

        var genreUniqueNameKey = Builders<Genre>.IndexKeys.Text(genre => genre.Name);
        var userInterestsUserIdKey = Builders<UserInterests>.IndexKeys.Text(ui => ui.UserId);

        var createGenreKeyModel = new CreateIndexModel<Genre>(genreUniqueNameKey,
            new CreateIndexOptions
            {
                Unique = true
            });

        var userInterestsUserIdKeyModel = new CreateIndexModel<UserInterests>(userInterestsUserIdKey,
            new CreateIndexOptions
            {
                Unique = true
            });

        genreCollection.Indexes.CreateOneAsync(createGenreKeyModel).GetAwaiter().GetResult();
        userInterestsCollection.Indexes.CreateOneAsync(userInterestsUserIdKeyModel).GetAwaiter().GetResult();
    }
}