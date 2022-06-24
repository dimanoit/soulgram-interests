using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class SearchMovieRoot
{
    [JsonPropertyName("page")] public string Page { get; init; } = null!;

    [JsonPropertyName("next")] public string? Next { get; init; }

    [JsonPropertyName("entries")] public int Entries { get; init; }

    [JsonPropertyName("results")] public ICollection<SearchMovieResult>? Results { get; init; }
}