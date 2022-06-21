namespace Soulgram.Interests.Application.Models.Request;

public record CreateInterestRequest
{
    public string Name { get; init; } = null!;
}