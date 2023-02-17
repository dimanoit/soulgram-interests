using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Models.Request.Interests;

public class CreateInterestsRequest
{
    public InterestGroupType[] Types { get; init; } = Array.Empty<InterestGroupType>();
}