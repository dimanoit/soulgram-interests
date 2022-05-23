using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands;

public class AddInterestsToUserCommand : IRequest
{
    public AddInterestsToUserCommand(UserInterestsRequest request)
    {
        Request = request;
    }

    public UserInterestsRequest Request { get; }
}

internal class AddInterestsToUserCommandHandler : IRequestHandler<AddInterestsToUserCommand>
{
    private readonly IUserInterestsRepository _repository;

    public AddInterestsToUserCommandHandler(IUserInterestsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddInterestsToUserCommand command, CancellationToken cancellationToken)
    {
        await _repository.AddInterestToUserInterests(command.Request.UserId, command.Request.Interests);
        return Unit.Value;
    }
}