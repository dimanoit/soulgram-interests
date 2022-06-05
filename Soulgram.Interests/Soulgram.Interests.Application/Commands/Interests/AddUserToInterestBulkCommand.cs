using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands.Interests;

public class AddUserToInterestBulkCommand : IRequest
{
    public AddUserToInterestBulkCommand(UserInterestsRequestBulk request)
    {
        Request = request;
    }

    public UserInterestsRequestBulk Request { get; }
}

internal class AddUserToInterestBulkCommandHandler : IRequestHandler<AddUserToInterestBulkCommand>
{
    private readonly IUserInterestsRepository _repository;

    public AddUserToInterestBulkCommandHandler(IUserInterestsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddUserToInterestBulkCommand command, CancellationToken cancellationToken)
    {
        await _repository.AddUserToInterestBulk(command.Request.UserId, command.Request.InterestId);
        return Unit.Value;
    }
}