using MediatR;
using Soulgram.Interests.Application.Interfaces;

namespace Soulgram.Interests.Application.Commands;

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

internal class AddGeneralInterestsToOneUserCommandHandler : IRequestHandler<AddInterestsToOneUserCommand>
{
    private readonly IUserInterestsRepository _repository;

    public AddGeneralInterestsToOneUserCommandHandler(IUserInterestsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddInterestsToOneUserCommand request, CancellationToken cancellationToken)
    {
        await _repository.AddInterestsToOneUser(request.UserId, request.InterestsIds);

        return Unit.Value;
    }
}