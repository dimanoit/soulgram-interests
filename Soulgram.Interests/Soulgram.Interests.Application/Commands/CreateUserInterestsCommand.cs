using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands;

public class CreateUserInterestsCommand : IRequest
{
    public CreateUserInterestsCommand(UserInterestsRequest request)
    {
        Request = request;
    }

    public UserInterestsRequest Request { get; }
}

internal class CreateUserInterestsCommandHandler : IRequestHandler<CreateUserInterestsCommand>
{
    private readonly IRepository<UserInterests> _repository;

    public CreateUserInterestsCommandHandler(IRepository<UserInterests> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateUserInterestsCommand command, CancellationToken cancellationToken)
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