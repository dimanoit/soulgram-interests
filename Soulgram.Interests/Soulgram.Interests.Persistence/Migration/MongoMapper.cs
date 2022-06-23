namespace Soulgram.Interests.Persistence.Migration.ModelMappers;

public class MongoMapper
{
    public void MapModels()
    {
        var mappers = new List<IModelMapper>
        {
            new GenreMapper(),
            new MovieMapper(),
            new InterestsMapper(),
            new UserFavoritesMapper()
        };


        mappers.ForEach(m => m.MapFields());
    }
}