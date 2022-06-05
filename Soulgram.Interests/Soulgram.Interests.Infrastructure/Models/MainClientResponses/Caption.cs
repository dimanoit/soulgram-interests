using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class Caption
{
    [JsonPropertyName("plainText")]
    public string PlainText { get; set; }

    [JsonPropertyName("__typename")]
    public string Typename { get; set; }
}