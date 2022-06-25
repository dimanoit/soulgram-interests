using MediatR;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Application.Queries.Interests;

public class GetAggregatedInterestsQuery : IRequest<ICollection<AggregatedInterests>>
{
    public GetAggregatedInterestsQuery(string userId)
    {
        UserId = userId;
    }
    public string UserId { get;  }
}

public class GetAggregatedInterestsQueryHandler :
    IRequestHandler<GetAggregatedInterestsQuery, ICollection<AggregatedInterests>>
{
    private readonly IUserFavoritesRepository _favoritesRepository;

    public GetAggregatedInterestsQueryHandler(
        IUserFavoritesRepository favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
    }

    public async Task<ICollection<AggregatedInterests>> Handle(
        GetAggregatedInterestsQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}