using Microsoft.Extensions.Options;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Converters;
using Soulgram.Interests.Infrastructure.Extensions;
using Soulgram.Interests.Infrastructure.Filters;
using Soulgram.Interests.Infrastructure.Models;

namespace Soulgram.Interests.Infrastructure.Clients;

public class OttClient : IMovieService
{
    private readonly HttpClient _httpClient;
    private readonly IMovieResponseFilter _movieResponseFilter;

    public OttClient(
        IOptions<OttClientSettings> settings,
        HttpClient httpClient,
        IMovieResponseFilter movieResponseFilter)
    {
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _movieResponseFilter = movieResponseFilter ?? throw new ArgumentException(nameof(movieResponseFilter));

        var baseAddress = $"https://{settings.Value.Host}";

        _httpClient.BaseAddress = new Uri(baseAddress);
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", settings.Value.Key);
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", settings.Value.Host);
    }

    public async Task<IEnumerable<string>> GetGenresAsync(CancellationToken cancellationToken)
    {
        var url = "getParams?param=genre";
        return await GetHttpResult<IEnumerable<string>>(url, cancellationToken);
    }

    public async Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(string name,
        CancellationToken cancellationToken)
    {
        var url = $"search?title={name}&page=1";
        var response = await GetHttpResult<MovieWithPageResponseModel?>(url, cancellationToken);

        if (response?.Results == null) return Enumerable.Empty<MovieSearchResponse>();

        return _movieResponseFilter
            .Filter(response.Results)
            .Select(r => r.ToMovieSearchResponse());
    }

    private async Task<T> GetHttpResult<T>(string uri, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(uri, cancellationToken);

        return await response.DeserializeStringAsync<T>(cancellationToken)
               ?? throw new InvalidOperationException();
    }
}