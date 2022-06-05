using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands;

public class AddUserToInterestCommand : IRequest
{
    public AddUserToInterestCommand(UserInterestRequest request)
    {
        Request = request;
    }

    public UserInterestRequest Request { get; }
}

internal class AddInterestsToUserCommandHandler : IRequestHandler<AddUserToInterestCommand>
{
    private readonly IUserInterestsRepository _repository;

    public AddInterestsToUserCommandHandler(IUserInterestsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddUserToInterestCommand command, CancellationToken cancellationToken)
    {
        await _repository.AddUserToInterest(command.Request.UserId, command.Request.InterestId);
        return Unit.Value;
    }
}