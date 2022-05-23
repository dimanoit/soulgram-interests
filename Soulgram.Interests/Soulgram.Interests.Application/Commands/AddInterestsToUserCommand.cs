using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Domain;

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
        var userInterest = new UserInterests
        {
            Interests = command.Request.Interests,
            UserId = command.Request.UserId
        };

        await _repository.InsertOneAsync(userInterest, cancellationToken);

        return Unit.Value;
    }
}