using MediatR;
using Soulgram.Interests.Application.Converters;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Application.Queries.Genres;
using Soulgram.Interests.Application.Queries.Movies;

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

        // var movies = await GetMoviesAggregatedSection(
        //     "favorites.MoviesIds",
        //     "favorites.GenresIds",
        //     cancellationToken);

        //return new[] {movies};
        throw new NotImplementedException();
    }

    private async Task<AggregatedInterests> GetMoviesAggregatedSection(
        string[] moviesIds,
        string[] genresIds,
        CancellationToken cancellationToken)
    {
        var aggregatedInterest = new AggregatedInterests
        {
            Name = "Movies",
            Items = new List<AggregatedInterestItem>()
        };

        var getMoviesQuery = new GetMoviesQuery(moviesIds);

        var movies = await _mediator.Send(getMoviesQuery, cancellationToken);
        var convertedMovies = movies
            ?.Where(m => m != null)
            .Select(m => m.ToAggregatedInterestItemValue())
            .ToArray() ?? Array.Empty<AggregatedInterestItemValue>();

        var movieNameItem = new AggregatedInterestItem
        {
            Name = "Names",
            Values = convertedMovies
        };

        aggregatedInterest.Items.Add(movieNameItem);


        var genresQuery = new GetGenresQuery(genresIds);
        var genresResult = await _mediator.Send(genresQuery, cancellationToken);
        var convertedGenres = genresResult
            ?.Where(g => g != null)
            .Select(g => g.ToAggregatedInterestItemValue())
            .ToArray() ?? Array.Empty<AggregatedInterestItemValue>();

        var genreItem = new AggregatedInterestItem
        {
            Name = "Genres",
            Values = convertedGenres
        };

        aggregatedInterest.Items.Add(genreItem);

        return aggregatedInterest;
    }
}