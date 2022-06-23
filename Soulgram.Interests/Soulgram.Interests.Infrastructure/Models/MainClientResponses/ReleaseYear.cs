using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class ReleaseYear
{
    [JsonPropertyName("year")]
    public int? Year { get; set; }

    [JsonPropertyName("endYear")]
    public object? EndYear { get; set; }

    [JsonPropertyName("__typename")]
    public string? Typename { get; set; }
}