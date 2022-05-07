using MediatR;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application;

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
    private readonly IMongoRepository<Genre> _genreRepository;

    public GetAllGenresQueryHandler(IMongoRepository<Genre> genreRepository)
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