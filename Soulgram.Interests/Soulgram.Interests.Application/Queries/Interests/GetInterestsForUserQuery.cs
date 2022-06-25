using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Application.Queries.Interests;

public class GetInterestsForUserQuery : IRequest<IEnumerable<InterestResponse>>
{
    public GetInterestsForUserQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}

internal class GetInterestsForUserQueryHandler
    : IRequestHandler<GetInterestsForUserQuery, IEnumerable<InterestResponse>>
{
    private readonly IMediator _mediator;
    private readonly IUserFavoritesRepository _userFavoritesRepository;

    public GetInterestsForUserQueryHandler(
        IMediator mediator,
        IUserFavoritesRepository userFavoritesRepository)
    {
        _mediator = mediator;
        _userFavoritesRepository = userFavoritesRepository;
    }

    public async Task<IEnumerable<InterestResponse>> Handle(
        GetInterestsForUserQuery request,
        CancellationToken cancellationToken)
    {
        var interestsIds = await _userFavoritesRepository.FindOneAsync(
            uf => uf.UserId == request.UserId,
            uf => uf.InterestsIds,
            cancellationToken) ?? Array.Empty<string>();

        if (!interestsIds.Any())
        {
            return Enumerable.Empty<InterestResponse>();
        }

        var getInterestsQuery = new GetInterestsQuery(interestsIds);

        return await _mediator.Send(getInterestsQuery, cancellationToken);
    }
}