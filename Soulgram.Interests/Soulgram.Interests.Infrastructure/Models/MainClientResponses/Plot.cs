using System.Text.Json.Serialization;

namespace Soulgram.Interests.Infrastructure.Models.MainClientResponses;

public class Plot
{
    [JsonPropertyName("plotText")]
    public PlotText? PlotText { get; set; }

    [JsonPropertyName("language")]
    public Language? Language { get; set; }

    [JsonPropertyName("__typename")]
    public string? Typename { get; set; }
}