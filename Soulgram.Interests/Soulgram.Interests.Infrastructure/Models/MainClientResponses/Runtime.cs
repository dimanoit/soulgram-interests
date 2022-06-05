using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class Runtime
{
    [JsonPropertyName("seconds")]
    public int Seconds { get; set; }

    [JsonPropertyName("__typename")]
    public string Typename { get; set; }
}