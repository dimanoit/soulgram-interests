using Microsoft.Extensions.Options;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Converters;
using Soulgram.Interests.Infrastructure.Extensions;
using Soulgram.Interests.Infrastructure.Models;
using Soulgram.Interests.Infrastructure.Models.HttpClientParams;

namespace Soulgram.Interests.Infrastructure.Clients.Implementation;

public class OttClient : IOttClient
{
    private readonly HttpClient _httpClient;

    public OttClient(
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

    public async Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(string name,
        CancellationToken cancellationToken)
    {
        var url = $"search?title={name}&page=1";
        var response = await _httpClient.GetHttpResult<MovieWithPageResponseModel?>(url, cancellationToken);

        if (response?.Results == null) return Enumerable.Empty<MovieSearchResponse>();

        return response.Results
            .Select(r => r.ToMovieSearchResponse());
    }
}