using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Commands.Interests;
using Soulgram.Interests.Application.Models.Request.Interests;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries.Interests;

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
    public async Task<IEnumerable<InterestNameResponse>> GetInterestsForUser(
        [FromRoute] string userId,
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
        [FromBody] CreateInterestsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateInterestsCommand(request);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPatch]
    public async Task AddInterestToUser(
        [FromBody] AddInterestToUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddUserToInterestsCommand(request);
        await _mediator.Send(command, cancellationToken);
    }
}