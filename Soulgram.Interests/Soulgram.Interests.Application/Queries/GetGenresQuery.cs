using System.Linq.Expressions;
using MediatR;
using Soulgram.Interests.Application.Interfaces;
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
    public GetAllGenresQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ICollection<GenreResponse>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Genre, bool>> filterExpression;
        var isPassedUserId = string.IsNullOrEmpty(request.UserId);
        if (isPassedUserId)
        {
            filterExpression = g => g.Id != null;
        }
        else
        {
            filterExpression = g => g.UsersIds.Contains(request.UserId);
        }

        var result = await _genreRepository.FilterByAsync(
            filterExpression,
            g => new GenreResponse(g.Name));

        return result;
    }
}