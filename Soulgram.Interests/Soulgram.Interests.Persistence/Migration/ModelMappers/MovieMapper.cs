using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Persistence.Migration.ModelMappers;

public class MovieMapper : IModelMapper
{
    public void MapFields()
    {
        BsonClassMap.RegisterClassMap<Movie>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(movie => movie.Id)
                .SetIdGenerator(new StringObjectIdGenerator())
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(movie => movie.ImdbId).SetIsRequired(true);
            cm.MapMember(movie => movie.Title).SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}