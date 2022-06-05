using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Infrastructure.Facades;

public interface IMovieFacade
{
    Task<ICollection<string>?> GetGenresAsync(CancellationToken cancellationToken);
    Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(string name, CancellationToken cancellationToken);
}