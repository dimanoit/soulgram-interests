namespace Soulgram.Interests.Application.Models.Request;

public record UserInterestRequest
{
    public string UserId { get; init; } = null!;
    public string InterestId { get; init; } = null!;
}