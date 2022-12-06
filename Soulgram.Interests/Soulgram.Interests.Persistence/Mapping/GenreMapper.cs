using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Soulgram.Interests.Domain;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Interests.Persistence.Migration.ModelMappers;

public class GenreMapper : IModelMapper
{
    public void MapFields()
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
    }
}