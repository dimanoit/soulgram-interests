namespace Soulgram.Interests.Application.Models.Request;

public record CreateUserInterestsRequest
{
    public string InterestName { get; init; }
}