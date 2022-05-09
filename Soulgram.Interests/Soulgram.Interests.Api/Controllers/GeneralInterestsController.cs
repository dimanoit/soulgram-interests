using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Interests.Application.Commands;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Queries;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeneralInterestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GeneralInterestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task AddGeneralInterests([FromBody] UserInterestsRequest request, CancellationToken cancellationToken)
    {
        var addUserInterestsCommand = new AddUserInterestsCommand(request);
        await _mediator.Send(addUserInterestsCommand, cancellationToken);
    }

    [HttpGet("{userId}")]
    public async Task<UserInterests?> GetInterests(string userId, CancellationToken cancellationToken)
    {
        var getGeneralInterestsQuery = new GetGeneralInterestsQuery(userId);
        return await _mediator.Send(getGeneralInterestsQuery, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<InterestType>> GetAllInterests(CancellationToken cancellationToken)
    {
        var getGeneralInterestsQuery = new GetAllGeneralInterestsQuery();
        return await _mediator.Send(getGeneralInterestsQuery, cancellationToken);
    }
}