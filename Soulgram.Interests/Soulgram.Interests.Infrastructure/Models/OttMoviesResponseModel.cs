using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models;

public class OttMoviesResponseModel
{
    [JsonPropertyName("page")] public int Page { get; init; }

    [JsonPropertyName("results")] public ICollection<MovieResponseModel>? Results { get; init; }
}