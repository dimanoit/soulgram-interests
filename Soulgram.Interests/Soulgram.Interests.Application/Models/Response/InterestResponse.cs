namespace Soulgram.Interests.Application.Models.Response;

public record InterestResponse
{
    public string Id { get; init; } = null!;
    public string Name { get; init; } = null!;
}