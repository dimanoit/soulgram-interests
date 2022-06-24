using Microsoft.Extensions.Logging;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Clients;

namespace Soulgram.Interests.Infrastructure.Facades;

public class MovieFacade : IMovieFacade
{
    private readonly ILogger<MovieFacade> _logger;
    private readonly IMovieDatabaseClient _mainMovieClient;
    private readonly IReserveMovieClient _reserveMovieClient;

    public MovieFacade(
        IReserveMovieClient client,
        IMovieDatabaseClient mainMovieClient,
        ILogger<MovieFacade> logger)
    {
        _reserveMovieClient = client;
        _mainMovieClient = mainMovieClient;
        _logger = logger;
    }

    public async Task<ICollection<string>?> GetGenresAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _mainMovieClient.GetGenresAsync(cancellationToken);
        }
        catch (TaskCanceledException taskCanceledException)
        {
            _logger.LogWarning(taskCanceledException, "{Client} had an exception timeout on", _mainMovieClient);
            return await _reserveMovieClient.GetGenresAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<MovieSearchResponse>?> GetMoviesByName(
        SearchMovieRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await _mainMovieClient.GetMoviesByName(request, cancellationToken);
        }
        catch (TaskCanceledException taskCanceledException)
        {
            _logger.LogWarning(taskCanceledException, "{Client} had an exception timeout on", _mainMovieClient);
            return await _reserveMovieClient.GetMoviesByName(request.Name, request.Page, cancellationToken);
        }
    }
}