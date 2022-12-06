using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Commands.Sports;
using Soulgram.Interests.Application.Queries.Sports;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Api.Controllers;

[Route("api/[controller]")]
public class SportController : ControllerBase
{
    private readonly IMediator _mediator;

    public SportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ICollection<Sport>> GetSportActivities(ICollection<string> sports, CancellationToken cancellationToken)
    {
        var getSportsQuery = new GetSportsQuery(sports);
        return await _mediator.Send(getSportsQuery, cancellationToken);
    }


    [HttpPost]
    public async Task UploadSportActivities(ICollection<string> sports, CancellationToken cancellationToken)
    {
        var uploadSportsCommand = new UploadSportsCommand(sports);
        await _mediator.Send(uploadSportsCommand, cancellationToken);
    }
}