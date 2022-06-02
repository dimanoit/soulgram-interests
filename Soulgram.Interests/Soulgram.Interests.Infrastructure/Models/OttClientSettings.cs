namespace Soulgram.Interests.Infrastructure;

public record OttClientSettings
{
    public string Key { get; init; } = null!;
    public string Host { get; init; } = null!;
}