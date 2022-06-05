namespace Soulgram.Interests.Infrastructure.Clients;

public interface IMovieDatabaseClient
{
    Task<ICollection<string>> GetGenresAsync(CancellationToken cancellationToken);
}