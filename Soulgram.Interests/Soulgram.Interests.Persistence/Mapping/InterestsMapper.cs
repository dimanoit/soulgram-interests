using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Soulgram.Interests.Domain;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Interests.Persistence.Migration.ModelMappers;

public class InterestsMapper : IModelMapper
{
    public void MapFields()
    {
        BsonClassMap.RegisterClassMap<Interest>(cm =>
        {
            cm.AutoMap();

            cm.MapIdMember(ui => ui.Id)
                .SetIsRequired(true)
                .SetIdGenerator(new StringObjectIdGenerator())
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(i => i.Type)
                .SetSerializer(new EnumSerializer<InterestGroupType>(BsonType.String));

            cm.SetIgnoreExtraElements(true);
        });
    }
}