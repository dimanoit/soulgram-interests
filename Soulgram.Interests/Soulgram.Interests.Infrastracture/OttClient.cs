using Microsoft.Extensions.Options;
using Soulgram.Interests.Application;

namespace Soulgram.Interests.Infrastracture;

public class OttClient : IMovieService
{
    private readonly HttpClient _httpClient;

    public OttClient(HttpClient httpClient, IOptions<OttClientSettings> settings)
    {
        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        var baseAddress = $"https://{settings.Value.Host}";

        _httpClient.BaseAddress = new Uri(baseAddress);
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", settings.Value.Key);
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", settings.Value.Host);
    }

    public async Task<IEnumerable<string>> GetGenresAsync(CancellationToken cancellationToken)
    {
        var url = "getParams?param=genre";
        var response = await _httpClient.GetAsync(url, cancellationToken);

        return await response.DeserializeStringAsync<IEnumerable<string>>(cancellationToken)
               ?? throw new InvalidOperationException();
    }
}