using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries.Movies;

public class GetUserMoviesQueryHandler : IRequestHandler<GetUserMoviesQuery, ICollection<MovieSearchResponse>?>
{
    private readonly IUserFavoritesRepository _favoritesRepository;
    private readonly IRepository<Movie> _repository;

    public GetUserMoviesQueryHandler(IRepository<Movie> repository, IUserFavoritesRepository favoritesRepository)
    {
        _repository = repository;
        _favoritesRepository = favoritesRepository;
    }

    public async Task<ICollection<MovieSearchResponse>?> Handle(GetUserMoviesQuery request, CancellationToken cancellationToken)
    {
        var moviesIds = await _favoritesRepository.FindOneAsync(
            uf => uf.UserId == request.UserId,
            favorites => favorites.MoviesIds,
            cancellationToken);

        if (!moviesIds.Any())
        {
            return null;
        }
        
        var movies = await _repository.FilterByAsync(
            m => moviesIds.Contains(m.Id),
            movie => new MovieSearchResponse
            {
                ImdbId = movie.ImdbId,
                Title = movie.Title,
                BriefDescription = movie.BriefDescription,
                ReleasedYear = movie.ReleasedYear,
                Genres = movie.GenresNames == null
                    ? Enumerable.Empty<GenreResponse>()
                    : movie.GenresNames.Select(m => new GenreResponse(m)),
                ImgUrls = movie.ImgUrls
            },
            cancellationToken);

        return movies;
    }
}