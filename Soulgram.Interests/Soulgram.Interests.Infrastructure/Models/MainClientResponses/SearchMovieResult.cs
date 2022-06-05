using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class SearchMovieResult
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("ratingsSummary")]
    public RatingsSummary RatingsSummary { get; set; }

    [JsonPropertyName("episodes")]
    public object Episodes { get; set; }

    [JsonPropertyName("primaryImage")]
    public PrimaryImage PrimaryImage { get; set; }

    [JsonPropertyName("titleType")]
    public TitleType TitleType { get; set; }

    [JsonPropertyName("genres")]
    public Genre[] Genres { get; set; }

    [JsonPropertyName("titleText")]
    public TitleText TitleText { get; set; }

    [JsonPropertyName("releaseYear")]
    public ReleaseYear ReleaseYear { get; set; }

    [JsonPropertyName("releaseDate")]
    public ReleaseDate ReleaseDate { get; set; }

    [JsonPropertyName("runtime")]
    public Runtime Runtime { get; set; }

    [JsonPropertyName("series")]
    public object Series { get; set; }

    [JsonPropertyName("meterRanking")]
    public object MeterRanking { get; set; }

    [JsonPropertyName("plot")]
    public Plot Plot { get; set; }
}