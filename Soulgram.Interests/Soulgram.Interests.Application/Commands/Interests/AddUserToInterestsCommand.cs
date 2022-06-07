using MediatR;

namespace Soulgram.Interests.Application.Commands.Interests;

public class AddUserToInterestsCommand : IRequest
{
    public AddUserToInterestsCommand(string userId, string[] interestsIds)
    {
        UserId = userId;
        InterestsIds = interestsIds;
    }

    public string UserId { get; }
    public string[] InterestsIds { get; }
}