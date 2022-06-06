using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class PrimaryImage
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("caption")]
    public Caption Caption { get; set; }

    [JsonPropertyName("__typename")]
    public string Typename { get; set; }
}