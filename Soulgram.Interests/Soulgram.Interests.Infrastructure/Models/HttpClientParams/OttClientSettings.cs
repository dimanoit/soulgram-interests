namespace Soulgram.Interests.Infrastructure.Models.HttpClientParams;

public record OttClientSettings : IHttpClientSettings
{
    public string Key { get; init; } = null!;
    public string Host { get; init; } = null!;
    public float TimeoutSeconds { get; init; }
}