using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application;

public interface IGenreRepository : IMongoRepository<Genre>
{
    Task AddUserIdToGenre(string genreId, string userId);
}