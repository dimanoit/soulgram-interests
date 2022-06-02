using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models;

public record MovieResponseModel
{
    [JsonPropertyName("imdbid")]
    public string ImdbId { get; init; } = null!;

    [JsonPropertyName("title")]
    public string Title { get; init; } = null!;

    [JsonPropertyName("synopsis")]
    public string? BriefDescription { get; init; }

    [JsonPropertyName("genre")]
    public IEnumerable<string>? Genre { get; init; }

    [JsonPropertyName("imageurl")]
    public IEnumerable<string>? ImgUrls { get; init; }

    [JsonPropertyName("released")]
    public int? ReleasedYear { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }
}