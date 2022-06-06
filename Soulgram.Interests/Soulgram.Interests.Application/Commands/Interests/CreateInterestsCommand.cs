using MediatR;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands.Interests;

public class CreateInterestsCommand : IRequest
{
    public CreateInterestsCommand(CreateInterestsRequest request)
    {
        Request = request;
    }

    public CreateInterestsRequest Request { get; }
}