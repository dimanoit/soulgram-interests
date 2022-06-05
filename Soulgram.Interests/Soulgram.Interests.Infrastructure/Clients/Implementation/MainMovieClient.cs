using Microsoft.Extensions.Options;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Converters;
using Soulgram.Interests.Infrastructure.Extensions;
using Soulgram.Interests.Infrastructure.Models;
using Soulgram.Interests.Infrastructure.Models.HttpClientParams;
using Soulgram.Interests.Infrastructure.Models.MainClientResponses;

namespace Soulgram.Interests.Infrastructure.Clients.Implementation;

public class MainMovieClient : IMovieDatabaseClient
{
    private readonly HttpClient _httpClient;

    public MainMovieClient(
        IOptions<MovieDatabaseClientSettings> settings,
        HttpClient httpClient)
    {
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.SetupWithClientSettings(settings.Value);
    }

    public async Task<ICollection<string>> GetGenresAsync(CancellationToken cancellationToken)
    {
        var url = "titles/utils/genres";
        var result = await _httpClient.GetHttpResult<GenresResponseModel>(url, cancellationToken);

        return result.Names;
    }

    public async Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(string name, CancellationToken cancellationToken)
    {
        var url = $"titles/search/title/{name}?info=base_info&limit=1&page=1&titleType=movie";
        var result = await _httpClient.GetHttpResult<SearchMovieRoot>(url, cancellationToken);

        if (result.Results.Count == 0) return Enumerable.Empty<MovieSearchResponse>();

        return result.Results.Select(r => r.ToMovieSearchResponse());
    }
}