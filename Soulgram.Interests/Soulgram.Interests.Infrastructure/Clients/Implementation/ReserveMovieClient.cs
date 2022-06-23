using Microsoft.Extensions.Options;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Converters;
using Soulgram.Interests.Infrastructure.Extensions;
using Soulgram.Interests.Infrastructure.Models;
using Soulgram.Interests.Infrastructure.Models.HttpClientParams;

namespace Soulgram.Interests.Infrastructure.Clients.Implementation;

public class ReserveMovieClient : IReserveMovieClient
{
    private readonly HttpClient _httpClient;
    private readonly string[] _movieTypes = {"movie", "tvMovie"};

    public ReserveMovieClient(
        IOptions<OttClientSettings> settings,
        HttpClient httpClient)
    {
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.SetupWithClientSettings(settings.Value);
    }

    public async Task<ICollection<string>> GetGenresAsync(CancellationToken cancellationToken)
    {
        var url = "getParams?param=genre";
        return await _httpClient.GetHttpResult<ICollection<string>>(url, cancellationToken);
    }

    public async Task<IEnumerable<MovieSearchResponse>?> GetMoviesByName(
        string name,
        int page,
        CancellationToken cancellationToken)
    {
        var url = $"search?title={name}&page={page}";
        var response = await _httpClient.GetHttpResult<OttMoviesResponseModel?>(url, cancellationToken);

        return response?.Results?.Count == 0
            ? Enumerable.Empty<MovieSearchResponse>()
            : response!.Results!
                .Where(m => m != null)
                .Where(mrm => _movieTypes.Contains(mrm.Type))
                .Select(r => r.ToMovieSearchResponse());
    }
}