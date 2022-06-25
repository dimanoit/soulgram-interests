using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class Language
{
    [JsonPropertyName("id")] public string? Id { get; set; }

    [JsonPropertyName("__typename")] public string? Typename { get; set; }
}