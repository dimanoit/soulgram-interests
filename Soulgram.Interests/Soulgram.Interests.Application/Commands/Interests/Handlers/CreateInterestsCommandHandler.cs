using MediatR;
using Soulgram.Interests.Application.Converters;
using Soulgram.Interests.Application.Interfaces;

namespace Soulgram.Interests.Application.Commands.Interests.Handlers;

internal class CreateInterestsCommandHandler : IRequestHandler<CreateInterestsCommand>
{
    private readonly IInterestsRepository _repository;

    public CreateInterestsCommandHandler(IInterestsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateInterestsCommand command, CancellationToken cancellationToken)
    {
        var interests = command.Request.Names
            .Select(n => n.ToInterest())
            .ToArray();

        await _repository.InsertManyAsync(interests, cancellationToken);
        return Unit.Value;
    }
}