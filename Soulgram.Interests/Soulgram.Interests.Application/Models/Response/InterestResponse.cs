namespace Soulgram.Interests.Application.Models.Response;

public record InterestResponse
{
    public string? Id { get; init; }
    public string Name { get; init; } = null!;
}