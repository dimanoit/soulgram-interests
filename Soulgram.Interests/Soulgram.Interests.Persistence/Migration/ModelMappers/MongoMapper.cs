namespace Soulgram.Interests.Persistence.Migration.ModelMappers;

public class MongoMapper
{
    public void MapModels()
    {
        new GenreMapper().MapFields();
        new UserInterestsMapper().MapFields();
    }
}