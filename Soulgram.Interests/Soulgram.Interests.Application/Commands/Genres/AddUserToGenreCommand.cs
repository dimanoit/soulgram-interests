using System.ComponentModel.DataAnnotations;
using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands.Genres;

public class AddUserToGenreCommand : IRequest
{
    public AddUserToGenreCommand(CreateGenreRequest bulkRequest)
    {
        BulkRequest = bulkRequest;
    }

    public CreateGenreRequest BulkRequest { get; }
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
        if (string.IsNullOrEmpty(command.BulkRequest.UserId))
        {
            throw new ValidationException("Request should contains user id");
        }

        var genreId = await _genreRepository.FindOneAsync(
            g => g.Name == command.BulkRequest.GenreName,
            g => g.Id,
            cancellationToken);

        if (string.IsNullOrEmpty(genreId))
        {
            var createGenreCommand = new CreateGenreCommand(command.BulkRequest);
            return await _mediator.Send(createGenreCommand, cancellationToken);
        }

        await _genreRepository.AddUserIdToGenre(genreId, command.BulkRequest.UserId);

        return Unit.Value;
    }
}