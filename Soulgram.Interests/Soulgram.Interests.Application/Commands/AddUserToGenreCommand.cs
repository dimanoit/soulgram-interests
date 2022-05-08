using MediatR;
using Soulgram.Interests.Application.Interfaces;

namespace Soulgram.Interests.Application.Commands;

public class AddUserToGenresCommand : IRequest
{
    public AddUserToGenresCommand(string userId, string genreName)
    {
        UserId = userId;
        GenreName = genreName;
    }

    public string GenreName { get; }
    public string UserId { get; }
}

internal class AddUserToGenreCommandHandler : IRequestHandler<AddUserToGenresCommand>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMediator _mediator;

    public AddUserToGenreCommandHandler(IGenreRepository genreRepository, IMediator mediator)
    {
        _genreRepository = genreRepository;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(AddUserToGenresCommand request, CancellationToken cancellationToken)
    {
        var genreId = await _genreRepository.FindOneAsync(
            g => g.Name == request.GenreName,
            g => g.Id,
            cancellationToken);

        if (string.IsNullOrEmpty(genreId))
        {
            var createGenreCommand = new CreateGenreWithUserCommand(request.UserId, request.GenreName);
            return await _mediator.Send(createGenreCommand, cancellationToken);
        }

        await _genreRepository.AddUserIdToGenre(genreId, request.UserId);

        return Unit.Value;
    }
}