namespace Soulgram.Interests.Application;

public interface IMovieService
{
    Task<IEnumerable<string>> GetGenresAsync(CancellationToken cancellationToken);
}