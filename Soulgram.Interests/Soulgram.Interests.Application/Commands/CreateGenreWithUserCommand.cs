using MediatR;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application;

public class CreateGenreWithUserCommand : IRequest
{
    public CreateGenreWithUserCommand(string userId, string genreName)
    {
        UserId = userId;
        GenreName = genreName;
    }

    public string UserId { get; }
    public string GenreName { get; }
}

internal class CreateGenreWithUserCommandHandler : IRequestHandler<CreateGenreWithUserCommand>
{
    private readonly IGenreRepository _genreRepository;

    public CreateGenreWithUserCommandHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Unit> Handle(CreateGenreWithUserCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre
        {
            Name = request.GenreName,
            UsersIds = new[] {request.UserId}
        };

        await _genreRepository.InsertOneAsync(genre, cancellationToken);

        return Unit.Value;
    }
}