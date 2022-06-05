using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class ReleaseDate
{
    [JsonPropertyName("day")]
    public int Day { get; set; }

    [JsonPropertyName("month")]
    public int Month { get; set; }

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("__typename")]
    public string Typename { get; set; }
}