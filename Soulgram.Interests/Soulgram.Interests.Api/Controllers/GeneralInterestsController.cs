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
        var command = new CreateGeneralInterestsBulkCommand(request);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPatch]
    public async Task AddGeneralInterests(
        [FromBody] UserInterestsRequest request,
        CancellationToken cancellationToken)
    {
        var addUserInterestsCommand = new AddUserToInterestCommand(request);
        await _mediator.Send(addUserInterestsCommand, cancellationToken);
    }

    [HttpPatch("bulk")]
    public async Task AddUserToInterestBulk(
        [FromBody] UserInterestsRequestBulk request,
        CancellationToken cancellationToken)
    {
        var addUserInterestsCommand = new AddUserToInterestBulkCommand(request);
        await _mediator.Send(addUserInterestsCommand, cancellationToken);
    }

    [HttpPatch("users/{userId}")]
    public async Task AddGeneralInterestsToOneUser(string userId, [FromBody] string[] interestsIds,
        CancellationToken cancellationToken)
    {
        var addUserInterestsCommand = new AddGeneralInterestsToOneUserCommand(userId, interestsIds);
        await _mediator.Send(addUserInterestsCommand, cancellationToken);
    }

    [HttpGet("{interestId}")]
    public async Task<GeneralInterestsResponse> GetInterest(
        string interestId,
        CancellationToken cancellationToken)
    {
        var getGeneralInterestsQuery = new GetGeneralInterestQuery(interestId);
        return await _mediator.Send(getGeneralInterestsQuery, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<GeneralInterestsResponse>> GetAllInterests(string? userId,
        CancellationToken cancellationToken)
    {
        var getGeneralInterestsQuery = new GetGeneralInterestsQuery(userId);
        return await _mediator.Send(getGeneralInterestsQuery, cancellationToken);
    }
}