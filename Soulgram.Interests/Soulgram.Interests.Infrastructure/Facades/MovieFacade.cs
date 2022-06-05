using Microsoft.Extensions.Logging;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Clients;

namespace Soulgram.Interests.Infrastructure.Facades;

public class MovieFacade : IMovieFacade
{
    private readonly ILogger<MovieFacade> _logger;
    private readonly IMovieDatabaseClient _movieDatabaseClient;
    private readonly IReserveMovieClient _reserveMovieClient;

    public MovieFacade(
        IReserveMovieClient client,
        IMovieDatabaseClient movieDatabaseClient,
        ILogger<MovieFacade> logger)
    {
        _reserveMovieClient = client;
        _movieDatabaseClient = movieDatabaseClient;
        _logger = logger;
    }

    public async Task<ICollection<string>?> GetGenresAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _reserveMovieClient.GetGenresAsync(cancellationToken);
        }
        catch (TaskCanceledException taskCanceledException)
        {
            _logger.LogWarning(taskCanceledException, "{Client} had an exception timeout on", _reserveMovieClient);
            return await _movieDatabaseClient.GetGenresAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(string name,
        CancellationToken cancellationToken)
    {
        try
        {
            return await _reserveMovieClient.GetMoviesByName(name, cancellationToken);
        }
        catch (TaskCanceledException taskCanceledException)
        {
            _logger.LogWarning(taskCanceledException, "{Client} had an exception timeout on", _reserveMovieClient);
            return await _movieDatabaseClient.GetMoviesByName(name, cancellationToken);
        }
    }
}