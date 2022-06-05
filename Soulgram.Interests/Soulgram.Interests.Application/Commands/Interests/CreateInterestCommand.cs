using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Interests;

public class CreateInterestCommand : IRequest
{
    public CreateInterestCommand(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

internal class CreateInterestCommandHandler : IRequestHandler<CreateInterestCommand>
{
    private readonly IRepository<Interest> _repository;

    public CreateInterestCommandHandler(IRepository<Interest> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateInterestCommand command, CancellationToken cancellationToken)
    {
        var userInterest = new Interest
        {
            Name = command.Name,
            UsersIds = Array.Empty<string>()
        };

        await _repository.InsertOneAsync(userInterest, cancellationToken);

        return Unit.Value;
    }
}