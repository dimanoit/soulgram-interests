using MediatR;
using Soulgram.Interests.Application.Converters;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands.Genres;

public class CreateGenreCommand : IRequest
{
    public CreateGenreCommand(CreateGenreRequest request)
    {
        Request = request;
    }

    public CreateGenreRequest Request { get; }
}

internal class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
{
    private readonly IGenreRepository _genreRepository;

    public CreateGenreCommandHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Unit> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
    {
        var genre = command.Request.ToGenre();
        await _genreRepository.InsertOneAsync(genre, cancellationToken);
        return Unit.Value;
    }
}