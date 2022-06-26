namespace Soulgram.Interests.Application.Models.Response;

public record AggregatedInterestItemValue
{
    public string Name { get; init; } = null!;
    public string? ImgUrl { get; init; }
}