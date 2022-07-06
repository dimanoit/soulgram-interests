using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Models.Request.Interests;

public record AddInterestToUserRequest(
    string UserId,
    string InterestId,
    InterestGroupType InterestType
    );
