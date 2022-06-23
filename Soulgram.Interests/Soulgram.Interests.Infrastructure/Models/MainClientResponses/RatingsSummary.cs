using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class RatingsSummary
{
    [JsonPropertyName("aggregateRating")]
    public float? AggregateRating { get; set; }

    [JsonPropertyName("voteCount")]
    public int? VoteCount { get; set; }

    [JsonPropertyName("__typename")]
    public string? Typename { get; set; }
}