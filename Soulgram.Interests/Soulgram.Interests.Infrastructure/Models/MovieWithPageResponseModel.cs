using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models;

public class MovieWithPageResponseModel
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("results")]
    public IEnumerable<MovieResponseModel>? Results { get; init; }
}