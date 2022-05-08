using MediatR;
using Soulgram.Interests.Application.Converters;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands;

public class CreateGenreWithUserCommand : IRequest
{
    public CreateGenreWithUserCommand(GenreWithUserRequest request)
    {
        Request = request;
    }

    public GenreWithUserRequest Request { get; }
}

internal class CreateGenreWithUserCommandHandler : IRequestHandler<CreateGenreWithUserCommand>
{
    private readonly IGenreRepository _genreRepository;

    public CreateGenreWithUserCommandHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Unit> Handle(CreateGenreWithUserCommand command, CancellationToken cancellationToken)
    {
        var genre = command.Request.ToGenre();
        await _genreRepository.InsertOneAsync(genre, cancellationToken);

        return Unit.Value;
    }
}