namespace Soulgram.Interests.Application.Models.Request;

public class UserInterestsRequestBulk
{
    public string[] UserId { get; init; } = null!;
    public string InterestId { get; init; } = null!;
}