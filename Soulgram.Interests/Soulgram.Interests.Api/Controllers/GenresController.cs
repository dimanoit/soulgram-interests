using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Commands.Genres;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Request.Genres;
using Soulgram.Interests.Application.Models.Response;
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
        var userGenresQuery = new GetUserGenresQuery(userId);
        return await _mediator.Send(userGenresQuery, cancellationToken);
    }

    [HttpPatch("{genreId}/users/{userId}")]
    public async Task AddGenreWithUser(
        [FromRoute] string genreId,
    [FromRoute] string userId,
        CancellationToken cancellationToken)
    {
        var addUserToGenreCommand = new AddGenreToUserCommand(genreId, userId);
        await _mediator.Send(addUserToGenreCommand, cancellationToken);
    }

    [HttpPost("from-client")]
    public async Task CreateByService(CancellationToken cancellationToken)
    {
        var createGenresByServiceCommand = new CreateGenresByServiceCommand();
        await _mediator.Send(createGenresByServiceCommand, cancellationToken);
    }
}