using MediatR;

namespace Soulgram.Interests.Application.Commands.Interests;

public class AddInterestsToOneUserCommand : IRequest
{
    public AddInterestsToOneUserCommand(string userId, string[] interestsIds)
    {
        UserId = userId;
        InterestsIds = interestsIds;
    }

    public string UserId { get; }
    public string[] InterestsIds { get; }
}