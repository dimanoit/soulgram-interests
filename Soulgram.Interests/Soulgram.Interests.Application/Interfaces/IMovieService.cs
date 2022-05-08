namespace Soulgram.Interests.Application.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<string>> GetGenresAsync(CancellationToken cancellationToken);
}