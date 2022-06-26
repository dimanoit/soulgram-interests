using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task AddUserIdToGenre(string genreId, string userId);
}