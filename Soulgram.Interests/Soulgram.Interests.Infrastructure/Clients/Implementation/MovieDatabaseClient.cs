using Microsoft.Extensions.Options;
using Soulgram.Interests.Infrastructure.Extensions;
using Soulgram.Interests.Infrastructure.Models;
using Soulgram.Interests.Infrastructure.Models.HttpClientParams;

namespace Soulgram.Interests.Infrastructure.Clients.Implementation;

public class MovieDatabaseClient : IMovieDatabaseClient
{
    private readonly HttpClient _httpClient;

    public MovieDatabaseClient(
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
}