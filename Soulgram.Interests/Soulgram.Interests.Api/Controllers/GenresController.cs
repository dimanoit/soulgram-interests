using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application;

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
    public async Task<ICollection<GenreResponse>> GetAllGenres(CancellationToken cancellationToken)
    {
        var getAllGenresQuery = new GetAllGenresQuery();

        var result = await _mediator.Send(getAllGenresQuery, cancellationToken);

        return result;
    }

    [HttpPost]
    public async Task CreateGenre(CancellationToken cancellationToken)
    {
        var getAllGenresQuery = new CreateGenreCommand("actions");

        var result = await _mediator.Send(getAllGenresQuery, cancellationToken);
    }
}