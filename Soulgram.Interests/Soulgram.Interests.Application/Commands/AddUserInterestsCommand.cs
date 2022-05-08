using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands;

public class AddUserInterestsCommand : IRequest
{
    public AddUserInterestsCommand(UserInterestsRequest request)
    {
        Request = request;
    }

    public UserInterestsRequest Request { get; }
}

internal class AddUserInterestsCommandHandler : IRequestHandler<AddUserInterestsCommand>
{
    private readonly IRepository<UserInterests> _repository;

    public AddUserInterestsCommandHandler(IRepository<UserInterests> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddUserInterestsCommand command, CancellationToken cancellationToken)
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