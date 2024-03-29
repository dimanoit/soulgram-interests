﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Soulgram.Interests.Domain;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Interests.Persistence.Migration.ModelMappers;

public class UserFavoritesMapper : IModelMapper
{
    public void MapFields()
    {
        BsonClassMap.RegisterClassMap<UserFavorites>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(userFavorites => userFavorites.Id)
                .SetIdGenerator(new StringObjectIdGenerator())
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(movie => movie.UserId).SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<InterestsIds>(cm =>
        {
            cm.AutoMap();

            cm.MapMember(iid => iid.Type)
                .SetSerializer(new EnumSerializer<InterestGroupType>(BsonType.String));
        });
    }
}