using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries.Genres;
using Soulgram.Interests.Application.Queries.Movies;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries.Interests;

// TODO need to refactor this class
public class GetAggregatedInterestsQuery : IRequest<ICollection<AggregatedInterests>>
{
    public GetAggregatedInterestsQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}

public class GetAggregatedInterestsQueryHandler :
    IRequestHandler<GetAggregatedInterestsQuery, ICollection<AggregatedInterests>>
{
    private readonly IUserFavoritesService _favoritesService;
    private readonly IMediator _mediator;

    public GetAggregatedInterestsQueryHandler(
        IUserFavoritesService favoritesService,
        IMediator mediator)
    {
        _favoritesService = favoritesService;
        _mediator = mediator;
    }

    public async Task<ICollection<AggregatedInterests>> Handle(
        GetAggregatedInterestsQuery request,
        CancellationToken cancellationToken)
    {
        var favorites = await _favoritesService.GetUserFavorites(request.UserId, cancellationToken);
        var result = new List<AggregatedInterests>();

        if (favorites == null)
        {
            return result;
        }

        var moviesAggregated = new AggregatedInterests()
        {
            Name = "Movies",
            Items = new List<AggregatedInterestItem>()
        };

        foreach (var interests in favorites.Interests)
        {
            switch (interests.Type)
            {
                case InterestGroupType.MovieGenre:
                    moviesAggregated.Items.Add(await GetMovieGenres(interests.Ids, cancellationToken));
                    break;

                case InterestGroupType.MovieName:
                    moviesAggregated.Items.Add(await GetMoviesNames(interests.Ids, cancellationToken));
                    break;
            }
        }

        result.Add(moviesAggregated);
        return result;
    }

    private async Task<AggregatedInterestItem> GetMoviesNames(
        string[] moviesIds,
        CancellationToken cancellationToken)
    {
        var movies = await _mediator.Send(
            new GetMoviesQuery(moviesIds), cancellationToken);

        var result = new AggregatedInterestItem
        {
            Name = "Names",
            Values = movies?.Select(x => new AggregatedInterestItemValue
            {
                Name = x.Title,
                ImgUrl = x.ImgUrls?.FirstOrDefault()
            }).ToList()
        };

        return result;
    }

    private async Task<AggregatedInterestItem> GetMovieGenres(
        string[] genresIds,
        CancellationToken cancellationToken)
    {
        var genres = await _mediator.Send(
            new GetGenresQuery(genresIds),
            cancellationToken);

        var result = new AggregatedInterestItem
        {
            Name = "Genres",
            Values = genres
                .Select(x => new AggregatedInterestItemValue { Name = x.Name!, })
                .ToList()
        };

        return result;
    }
}