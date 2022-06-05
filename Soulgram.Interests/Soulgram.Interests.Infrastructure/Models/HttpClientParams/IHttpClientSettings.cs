namespace Soulgram.Interests.Infrastructure.Models.HttpClientParams;

public interface IHttpClientSettings
{
    string Key { get; init; }
    string Host { get; init; }

    float TimeoutSeconds { get; init; }
}