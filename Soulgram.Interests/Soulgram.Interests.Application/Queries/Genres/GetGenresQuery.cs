using System.Linq.Expressions;
using MediatR;
using Soulgram.Interests.Application.Commands.Genres;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetGenresQuery : IRequest<ICollection<GenreResponse>>
{
    public GetGenresQuery(string? userId)
    {
        UserId = userId;
    }

    public string? UserId { get; }
}

internal class GetAllGenresQueryHandler : IRequestHandler<GetGenresQuery, ICollection<GenreResponse>>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMediator _mediator;
    private readonly IMovieService _movieService;

    public GetAllGenresQueryHandler(
        IGenreRepository genreRepository,
        IMovieService movieService,
        IMediator mediator)
    {
        _genreRepository = genreRepository;
        _movieService = movieService;
        _mediator = mediator;
    }

    public async Task<ICollection<GenreResponse>> Handle(
        GetGenresQuery request,
        CancellationToken cancellationToken)
    {
        var isPassedUserId = !string.IsNullOrEmpty(request.UserId);
        var filterExpression = GetFilterExpression(request, isPassedUserId);

        var result = await _genreRepository.FilterByAsync(
            filterExpression,
            g => new GenreResponse(g.Name));

        if (result.Count != 0 || isPassedUserId)
        {
            return result;
        }

        var genres = await _movieService.GetGenresAsync(cancellationToken);
        if (genres.Count <= 0)
        {
            return result;
        }

        CreateGenres(genres);

        return genres.Select(n => new GenreResponse(n)).ToArray();
    }

    private static Expression<Func<Genre, bool>> GetFilterExpression(GetGenresQuery request, bool isPassedUserId)
    {

        Expression<Func<Genre, bool>> filterExpression;
        if (isPassedUserId)
        {
            filterExpression = g => g.UsersIds.Contains(request.UserId);
        }
        else
        {
            filterExpression = g => g.Id != null;
        }

        return filterExpression;
    }

    private void CreateGenres(ICollection<string> genres)
    {
        var createGenresBulkRequest = new CreateGenresBulkRequest
        {
            GenreName = genres.ToArray()
        };

        var createGenresCommand = new CreateGenresCommand(createGenresBulkRequest);

        Task.Run(() =>
            _mediator.Send(createGenresCommand)
                .GetAwaiter()
                .GetResult()
        );
    }
}