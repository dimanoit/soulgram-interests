using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Application.Interfaces;

public interface IMovieService
{
    Task<ICollection<string>> GetGenresAsync(CancellationToken cancellationToken);

    Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(string name, CancellationToken cancellationToken);
}