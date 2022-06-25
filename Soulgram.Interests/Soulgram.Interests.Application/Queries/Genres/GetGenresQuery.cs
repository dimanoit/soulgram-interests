using System.Linq.Expressions;
using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries.Genres;

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

    public GetAllGenresQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ICollection<GenreResponse>> Handle(
        GetGenresQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Genre, bool>> filterExpression = !string.IsNullOrEmpty(request.UserId)
            ? g => g.UsersIds.Contains(request.UserId)
            : g => g.Id != null;

        return await _genreRepository.FilterByAsync(
            filterExpression,
            g => new GenreResponse(g.Id!, g.Name),
            cancellationToken);
    }
}