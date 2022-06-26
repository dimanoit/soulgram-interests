namespace Soulgram.Interests.Application.Models.Response;

public record AggregatedInterests
{
    public string Name { get; init; } = null!;
    public ICollection<AggregatedInterestItem>? Items { get; init; }
}