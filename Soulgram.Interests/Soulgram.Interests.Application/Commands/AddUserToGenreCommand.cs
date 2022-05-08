using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands;

public class AddUserToGenreCommand : IRequest
{
    public AddUserToGenreCommand(GenreWithUserRequest request)
    {
        Request = request;
    }

    public GenreWithUserRequest Request { get; }
}

internal class AddUserToGenreCommandHandler : IRequestHandler<AddUserToGenreCommand>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMediator _mediator;

    public AddUserToGenreCommandHandler(IGenreRepository genreRepository, IMediator mediator)
    {
        _genreRepository = genreRepository;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(AddUserToGenreCommand command, CancellationToken cancellationToken)
    {
        var genreId = await _genreRepository.FindOneAsync(
            g => g.Name == command.Request.GenreName,
            g => g.Id,
            cancellationToken);

        if (string.IsNullOrEmpty(genreId))
        {
            var createGenreCommand = new CreateGenreWithUserCommand(command.Request);
            return await _mediator.Send(createGenreCommand, cancellationToken);
        }

        await _genreRepository.AddUserIdToGenre(genreId, command.Request.UserId);

        return Unit.Value;
    }
}