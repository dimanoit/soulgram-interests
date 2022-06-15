using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Commands.Interests;
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

    [HttpGet("{userId}")]
    public async Task<IEnumerable<InterestResponse>> GetInterestsForUser(
        string userId,
        CancellationToken cancellationToken)
    {
        var query = new GetInterestsForUserQuery(userId);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<InterestResponse>> GetInterests(
        [FromQuery] string[] interestsIds,
        CancellationToken cancellationToken)
    {
        var query = new GetInterestsQuery(interestsIds);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task CreateInterests(
        CreateInterestsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateInterestsCommand(request);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPatch("users/{userId}")]
    public async Task AddInterestsToUser(
        string userId,
        [FromBody] string[] interestsIds,
        CancellationToken cancellationToken)
    {
        var command = new AddUserToInterestsCommand(userId, interestsIds);
        await _mediator.Send(command, cancellationToken);
    }
}