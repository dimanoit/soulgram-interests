using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class SearchMovieResult
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;

    [JsonPropertyName("ratingsSummary")] public RatingsSummary? RatingsSummary { get; init; }

    [JsonPropertyName("episodes")] public object? Episodes { get; init; }

    [JsonPropertyName("primaryImage")] public PrimaryImage? PrimaryImage { get; init; }

    [JsonPropertyName("titleType")] public TitleType? TitleType { get; init; }

    [JsonPropertyName("genres")] public GenreAggregated? GenreAggregated { get; init; }

    [JsonPropertyName("titleText")] public TitleText TitleText { get; init; } = null!;

    [JsonPropertyName("releaseYear")] public ReleaseYear? ReleaseYear { get; init; }

    [JsonPropertyName("releaseDate")] public ReleaseDate? ReleaseDate { get; init; }

    [JsonPropertyName("runtime")] public Runtime? Runtime { get; init; }

    [JsonPropertyName("series")] public object? Series { get; init; }

    [JsonPropertyName("meterRanking")] public object? MeterRanking { get; init; }

    [JsonPropertyName("plot")] public Plot? Plot { get; init; }
}