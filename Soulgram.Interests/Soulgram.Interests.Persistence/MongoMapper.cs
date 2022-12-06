using Soulgram.Interests.Persistence.Migration.ModelMappers;
using Soulgram.Mongo.Repository;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Interests.Persistence;

public class MongoMapper : FieldMapperBase
{
    public override IEnumerable<IModelMapper> GetModelMappers() =>
       new IModelMapper[]
       {
            new GenreMapper(),
            new MovieMapper(),
            new InterestsMapper(),
            new UserFavoritesMapper()
       };
}