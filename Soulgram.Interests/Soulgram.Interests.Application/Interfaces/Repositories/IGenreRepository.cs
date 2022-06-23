using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces;

public interface IGenreRepository : IRepository<Genre>
{
    Task AddUserIdToGenre(string genreId, string userId);
}