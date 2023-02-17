using MediatR;
using Soulgram.Interests.Application.Converters;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Request.Interests;

namespace Soulgram.Interests.Application.Commands.Interests;

public record CreateInterestsCommand(CreateInterestsRequest Request) : IRequest;
internal class CreateInterestsCommandHandler : IRequestHandler<CreateInterestsCommand>
{
    private readonly IInterestsRepository _repository;

    public CreateInterestsCommandHandler(IInterestsRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateInterestsCommand command, CancellationToken cancellationToken)
    {
        var interests = command.Request.Types
            .Select(n => n.ToInterest())
            .ToArray();

        await _repository.InsertManyAsync(interests, cancellationToken);
    }
}