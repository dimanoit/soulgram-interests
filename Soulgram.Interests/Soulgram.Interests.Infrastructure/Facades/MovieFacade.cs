using Microsoft.Extensions.Logging;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Clients;

namespace Soulgram.Interests.Infrastructure.Facades;

public class MovieFacade : IMovieFacade
{
    private readonly ILogger<MovieFacade> _logger;
    private readonly IMovieDatabaseClient _movieDatabaseClient;
    private readonly IOttClient _ottClient;

    public MovieFacade(
        IOttClient client,
        IMovieDatabaseClient movieDatabaseClient,
        ILogger<MovieFacade> logger)
    {
        _ottClient = client;
        _movieDatabaseClient = movieDatabaseClient;
        _logger = logger;
    }

    public async Task<ICollection<string>?> GetGenresAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _ottClient.GetGenresAsync(cancellationToken);
        }
        catch (TaskCanceledException taskCanceledException)
        {
            _logger.LogWarning(taskCanceledException, "{Client} had an exception timeout on", _ottClient);
            return await _movieDatabaseClient.GetGenresAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(string name,
        CancellationToken cancellationToken)
    {
        return await _ottClient.GetMoviesByName(name, cancellationToken);
    }
}