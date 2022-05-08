using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Models.Request;

public record UserInterestsRequest
{
    public string UserId { get; init; } = null!;
    public InterestType[] Interests { get; init; } = null!;
}