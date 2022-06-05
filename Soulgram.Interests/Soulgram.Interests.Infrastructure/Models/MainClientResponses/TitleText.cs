using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class TitleText
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("__typename")]
    public string Typename { get; set; }
}