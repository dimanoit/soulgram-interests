using MediatR;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Application.Queries.Interests;

public class GetInterestsForUserQuery : IRequest<IEnumerable<InterestNameResponse>>
{
    public GetInterestsForUserQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}

internal class GetInterestsForUserQueryHandler
    : IRequestHandler<GetInterestsForUserQuery, IEnumerable<InterestNameResponse>>
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

    public async Task<IEnumerable<InterestNameResponse>> Handle(
        GetInterestsForUserQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _userFavoritesRepository.Get(
            request.UserId,
            uf => uf.Interests.Select(i => new InterestNameResponse
            {
                Name = i.Type.ToString()
            })
            , cancellationToken);


        return response ?? Enumerable.Empty<InterestNameResponse>();
    }
}