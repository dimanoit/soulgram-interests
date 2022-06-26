using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Request.Genres;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Genres;

public class CreateGenresCommand : IRequest
{
    public CreateGenresCommand(CreateGenresBulkRequest bulkRequest)
    {
        BulkRequest = bulkRequest;
    }

    public CreateGenresBulkRequest BulkRequest { get; }
}

internal class CreateGenresCommandHandler : IRequestHandler<CreateGenresCommand>
{
    private readonly IGenreRepository _genreRepository;

    public CreateGenresCommandHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Unit> Handle(CreateGenresCommand command, CancellationToken cancellationToken)
    {
        var genres = command.BulkRequest.GenreName.Select(name =>
        {
            var projected = new Genre
            {
                Name = name
            };

            if (!string.IsNullOrEmpty(command.BulkRequest.UserId))
            {
                projected.UsersIds = new[] {command.BulkRequest.UserId};
            }

            return projected;
        }).ToArray();

        await _genreRepository.InsertManyAsync(genres, cancellationToken);
        return Unit.Value;
    }
}