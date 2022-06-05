using MediatR;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands;

public class CreateInterestsCommand : IRequest
{
    public CreateInterestsCommand(CreateInterestBulkRequest request)
    {
        Request = request;
    }

    public CreateInterestBulkRequest Request { get; }
}

internal class CreateGeneralInterestsBulkCommandHandler : IRequestHandler<CreateInterestsCommand>
{
    private readonly IMediator _mediator;

    public CreateGeneralInterestsBulkCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(CreateInterestsCommand command, CancellationToken cancellationToken)
    {
        var commands = command.Request
            .Names
            .Select(name => new CreateInterestCommand(name))
            .Select(c => _mediator.Send(c, cancellationToken));

        await Task.WhenAll(commands);

        return Unit.Value;
    }
}