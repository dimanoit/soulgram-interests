using MediatR;
using Soulgram.Interests.Application.Interfaces;

namespace Soulgram.Interests.Application.Queries;

public class GetMyGenresQuery : IRequest<ICollection<GenreResponse>>
{
    public GetMyGenresQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}

internal class GetAllGenresQueryHandler : IRequestHandler<GetMyGenresQuery, ICollection<GenreResponse>>
{
    private readonly IGenreRepository _genreRepository;

    public GetAllGenresQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ICollection<GenreResponse>> Handle(GetMyGenresQuery request, CancellationToken cancellationToken)
    {
        var result = await _genreRepository
            .FilterByAsync(
                g => g.UsersIds.Contains(request.UserId),
                g => new GenreResponse(g.Name));

        return result;
    }
}