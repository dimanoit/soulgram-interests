using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Infrastructure.Clients;

public interface IReserveMovieClient
{
    Task<ICollection<string>> GetGenresAsync(CancellationToken cancellationToken);

    Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(string name,
        CancellationToken cancellationToken);
}