using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application;
using Soulgram.Interests.Application.Commands.Genres;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries;
using Soulgram.Interests.Application.Queries.Genres;

namespace Soulgram.Interests.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ICollection<GenreResponse>> GetGenres(
        [FromQuery] string? userId,
        CancellationToken cancellationToken)
    {
        var getAllGenresQuery = new GetGenresQuery(userId);
        return await _mediator.Send(getAllGenresQuery, cancellationToken);
    }

    [HttpPost]
    public async Task AddGenreWithUser(
        [FromBody] CreateGenreRequest request,
        CancellationToken cancellationToken)
    {
        var addUserToGenreCommand = new AddUserToGenreCommand(request);
        await _mediator.Send(addUserToGenreCommand, cancellationToken);
    }

    [HttpPost("collection")]
    public async Task AddGenresWithUser(
        [FromBody] GenresWithUserRequest request,
        CancellationToken cancellationToken)
    {
        var addUserToGenreCommand = new AddUserToGenresCommand(request);
        await _mediator.Send(addUserToGenreCommand, cancellationToken);
    }

    [HttpPost("from-client")]
    public async Task CreateByService(CancellationToken cancellationToken)
    {
        var createGenresByServiceCommand = new CreateGenresByServiceCommand();
        await _mediator.Send(createGenresByServiceCommand, cancellationToken);
    }
}