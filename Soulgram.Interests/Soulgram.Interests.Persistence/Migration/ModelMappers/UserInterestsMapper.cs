using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Persistence.Migration.ModelMappers;

public class UserInterestsMapper : IModelMapper
{
    public void MapFields()
    {
        BsonClassMap.RegisterClassMap<UserInterests>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(ui => ui.Id)
                .SetIdGenerator(new StringObjectIdGenerator())
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(ui => ui.Id).SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}