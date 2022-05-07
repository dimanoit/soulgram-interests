using MediatR;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application;

public class GetAllGenresQuery : IRequest<ICollection<GenreResponse>>
{
}

internal class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, ICollection<GenreResponse>>
{
    private readonly IMongoRepository<Genre> _genreRepository;

    public GetAllGenresQueryHandler(IMongoRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ICollection<GenreResponse>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var result = await _genreRepository
            .FilterByAsync(_ => true, g => new GenreResponse(g.Name));

        return result;
    }
}