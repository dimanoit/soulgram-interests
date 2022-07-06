namespace Soulgram.Interests.Application.Models.Response;

public record AggregatedInterestItem
{
    public string Name { get; init; } = null!;
    public List<AggregatedInterestItemValue>? Values { get; init; }
}