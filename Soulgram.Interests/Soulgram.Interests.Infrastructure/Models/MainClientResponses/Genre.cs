using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class Genre
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("__typename")]
    public string Typename { get; set; }

    [JsonPropertyName("genres")]
    public List<Domain.Genre> Genres { get; set; }
}