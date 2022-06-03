using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Commands;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries;

namespace Soulgram.Interests.Api.Controllers;

[ApiController]
[Route("api/general-interests")]
public class GeneralInterestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GeneralInterestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task CreateGeneralInterests([FromBody] CreateUserInterestsRequest request,
        CancellationToken cancellationToken)
    {
        var addUserInterestsCommand = new CreateUserInterestsCommand(request);
        await _mediator.Send(addUserInterestsCommand, cancellationToken);
    }

    [HttpPost("bulk")]
    public async Task CreateGeneralInterestsBulk([FromBody] CreateUserInterestsRequest[] request,
        CancellationToken cancellationToken)
    {
        var commands = request.Select(r => new CreateUserInterestsCommand(r));
        foreach (var c in commands) await _mediator.Send(c, cancellationToken);
    }

    [HttpPatch]
    public async Task AddGeneralInterests(
        [FromBody] UserInterestsRequest request,
        CancellationToken cancellationToken)
    {
        var addUserInterestsCommand = new AddUserToInterestCommand(request);
        await _mediator.Send(addUserInterestsCommand, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<GeneralInterestsResponse>> GetAllInterests(CancellationToken cancellationToken)
    {
        var getGeneralInterestsQuery = new GetAllGeneralInterestsQuery();
        return await _mediator.Send(getGeneralInterestsQuery, cancellationToken);
    }
}