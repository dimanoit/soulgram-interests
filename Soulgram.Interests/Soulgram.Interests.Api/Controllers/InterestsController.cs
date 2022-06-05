using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Commands;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries;

namespace Soulgram.Interests.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InterestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task CreateInterest(
        [FromBody] CreateInterestRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateInterestCommand(request.Name);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPost("bulk")]
    public async Task CreateInterests(
        CreateInterestBulkRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateInterestsCommand(request);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPatch]
    public async Task AddUserToInterest(
        [FromBody] UserInterestRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddUserToInterestCommand(request);
        await _mediator.Send(command, cancellationToken);
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
    public async Task AddInterestsToOneUser(
        string userId,
        [FromBody] string[] interestsIds,
        CancellationToken cancellationToken)
    {
        var command = new AddInterestsToOneUserCommand(userId, interestsIds);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpGet("{interestId}")]
    public async Task<InterestResponse> GetInterest(
        string interestId,
        CancellationToken cancellationToken)
    {
        var query = new GetInterestQuery(interestId);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<InterestResponse>> GetInterests(
        string? userId,
        CancellationToken cancellationToken)
    {
        var query = new GetInterestsQuery(userId);
        return await _mediator.Send(query, cancellationToken);
    }
}