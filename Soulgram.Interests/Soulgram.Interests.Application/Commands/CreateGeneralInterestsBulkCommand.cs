using MediatR;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands;

public class CreateGeneralInterestsBulkCommand : IRequest
{
    public CreateGeneralInterestsBulkCommand(CreateUserInterestsRequest[] requests)
    {
        Requests = requests;
    }

    public CreateUserInterestsRequest[] Requests { get; }
}

internal class CreateGeneralInterestsBulkCommandHandler : IRequestHandler<CreateGeneralInterestsBulkCommand>
{
    private readonly IMediator _mediator;

    public CreateGeneralInterestsBulkCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(CreateGeneralInterestsBulkCommand command, CancellationToken cancellationToken)
    {
        var commands = command.Requests
            .Select(x => new CreateUserInterestsCommand(x))
            .Select(x => _mediator.Send(x, cancellationToken));

        await Task.WhenAll(commands);

        return Unit.Value;
    }
}