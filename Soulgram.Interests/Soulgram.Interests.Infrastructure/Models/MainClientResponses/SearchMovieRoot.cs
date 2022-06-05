using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class SearchMovieRoot
{
    [JsonPropertyName("page")]
    public string Page { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("entries")]
    public int Entries { get; set; }

    [JsonPropertyName("results")]
    public ICollection<SearchMovieResult> Results { get; set; }
}