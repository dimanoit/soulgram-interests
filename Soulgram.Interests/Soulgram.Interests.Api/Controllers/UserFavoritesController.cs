using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries.Interests;

namespace Soulgram.Interests.Api.Controllers;

[ApiController]
[Route("api/user-favorites")]
public class UserFavoritesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserFavoritesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    public async Task<ICollection<AggregatedInterests>> GetAggregatedInterests(
        [FromRoute] string userId,
        CancellationToken cancellationToken)
    {
        var query = new GetAggregatedInterestsQuery(userId);
        return await _mediator.Send(query, cancellationToken);
    }
}