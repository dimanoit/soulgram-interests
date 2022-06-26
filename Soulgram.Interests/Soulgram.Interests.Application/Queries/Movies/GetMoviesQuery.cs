using MediatR;
using Soulgram.Interests.Application.Converters;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries.Movies;

public record GetMoviesQuery(string[] moviesIds) 
    : IRequest<ICollection<MovieSearchResponse>?>;

public class GetMoviesQueryHandler 
    : IRequestHandler<GetMoviesQuery, ICollection<MovieSearchResponse>?>
{
    private readonly IRepository<Movie> _repository;

    public GetMoviesQueryHandler(IRepository<Movie> repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<MovieSearchResponse>?> Handle(
        GetMoviesQuery request,
        CancellationToken cancellationToken)
    {
        if (!request.moviesIds.Any())
        {
            return null;
        }
        
        var movies = await _repository.FilterByAsync(
            m => request.moviesIds.Contains(m.Id),
            movie => movie.ToMovieSearchResponse(),
            cancellationToken);

        return movies;
    }
}