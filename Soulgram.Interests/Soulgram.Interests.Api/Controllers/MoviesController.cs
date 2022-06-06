using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries;

namespace Soulgram.Interests.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<MovieSearchResponse>> SearchByName(
        [FromQuery] SearchMovieRequest request,
        CancellationToken cancellationToken)
    {
        var query = new SearchMoviesByNameQuery(request);
        return await _mediator.Send(query, cancellationToken);
    }
}