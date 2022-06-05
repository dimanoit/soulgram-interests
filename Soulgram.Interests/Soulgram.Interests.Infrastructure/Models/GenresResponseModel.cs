using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models;

public record GenresResponseModel
{
    [JsonPropertyName("results")]
    public ICollection<string> Names { get; init; } = null!;
}