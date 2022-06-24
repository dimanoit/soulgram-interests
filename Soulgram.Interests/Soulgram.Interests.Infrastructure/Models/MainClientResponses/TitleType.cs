using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class TitleType
{
    [JsonPropertyName("text")] public string? Text { get; set; }

    [JsonPropertyName("id")] public string? Id { get; set; }

    [JsonPropertyName("isSeries")] public bool? IsSeries { get; set; }

    [JsonPropertyName("isEpisode")] public bool? IsEpisode { get; set; }

    [JsonPropertyName("__typename")] public string? Typename { get; set; }
}