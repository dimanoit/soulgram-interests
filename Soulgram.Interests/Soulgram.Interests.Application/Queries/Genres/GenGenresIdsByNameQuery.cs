using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries.Genres;

public record GenGenresIdsByNameQuery(ICollection<string>? Names) 
    : IRequest<Dictionary<string, string?>?>;

public class GenGenresIdsByNameQueryHandler 
    : IRequestHandler<GenGenresIdsByNameQuery, Dictionary<string, string?>?>
{
    private readonly IRepository<Genre> _genreRepository;

    public GenGenresIdsByNameQueryHandler(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Dictionary<string, string?>?> Handle(
        GenGenresIdsByNameQuery command,
        CancellationToken cancellationToken)
    {
        if (command.Names == null)
        {
            return null;
        }

        var result = await _genreRepository
            .FilterByAsync(
                genre => command.Names.Contains(genre.Name),
                genre => genre.Name,
                cancellationToken);

        return command.Names.ToDictionary(
            name => name,
            name => result.FirstOrDefault(genreName => genreName == name));
    }
}