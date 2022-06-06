using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class GenreAggregated
{
    [JsonPropertyName("genres")]
    public Genre[]? Genres { get; init; }
}