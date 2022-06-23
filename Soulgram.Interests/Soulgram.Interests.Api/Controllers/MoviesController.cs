using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Commands.Movies;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Request.Movies;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries;
using Soulgram.Interests.Application.Queries.Movies;

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

    [HttpGet("{userId}")]
    public async Task<IEnumerable<MovieSearchResponse>?> GetUserMovies(
        [FromRoute] string userId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserMoviesQuery(userId);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task AddMovieToUser(
        [FromBody] AddMovieRequest movieRequest,
        CancellationToken cancellationToken)
    {
        var command = new AddMovieCommand(movieRequest);
        await _mediator.Send(command, cancellationToken);
    }
}