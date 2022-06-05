using Soulgram.Interests.Infrastructure.Models.HttpClientParams;

namespace Soulgram.Interests.Infrastructure.Extensions;

public static class HttpClientExtension
{
    public static async Task<T> GetHttpResult<T>(
        this HttpClient client,
        string uri,
        CancellationToken cancellationToken)
    {
        var response = await client.GetAsync(uri, cancellationToken);

        return await response.DeserializeStringAsync<T>(cancellationToken)
               ?? throw new InvalidOperationException();
    }

    public static void SetupWithClientSettings(
        this HttpClient client,
        IHttpClientSettings settings)
    {
        var baseAddress = $"https://{settings.Host}";

        client.BaseAddress = new Uri(baseAddress);
        client.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds);
        client.DefaultRequestHeaders.Add("x-rapidapi-key", settings.Key);
        client.DefaultRequestHeaders.Add("x-rapidapi-host", settings.Host);
    }
}