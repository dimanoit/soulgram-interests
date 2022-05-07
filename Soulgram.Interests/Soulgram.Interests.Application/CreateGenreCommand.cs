using MediatR;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application;

public class CreateGenreCommand : IRequest
{
    public CreateGenreCommand(string genreName)
    {
        GenreName = genreName;
    }

    public string GenreName { get; }
}

internal class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
{
    public IMongoRepository<Genre> _genreRepository;

    public CreateGenreCommandHandler(IMongoRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Unit> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre
        {
            Name = request.GenreName
        };

        await _genreRepository.InsertOneAsync(genre);

        return Unit.Value;
    }
}