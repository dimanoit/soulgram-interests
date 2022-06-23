using MediatR;
using Soulgram.Interests.Application.Commands.Genres;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Request.Genres;
using Soulgram.Interests.Application.Queries.Genres;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Movies.Handlers;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Movie> _repository;

    public CreateMovieCommandHandler(IMediator mediator, IRepository<Movie> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            ImdbId = command.Request.ImdbId,
            Title = command.Request.Title,
            BriefDescription = command.Request.BriefDescription,
            ReleasedYear = command.Request.ReleasedYear,
            ImgUrls = command.Request.ImgUrls,
            GenresNames = command.Request.Genres,
            UsersIds = new[] {command.Request.UserId!}
        };

        await CreateGenresIfNoExists(movie.GenresNames?.ToArray());

        await _repository.InsertOneAsync(movie, cancellationToken);

        return Unit.Value;
    }

    private async Task CreateGenresIfNoExists(ICollection<string>? genresNames)
    {
        var query = new GenGenresIdsByNameQuery(genresNames);
        var genres = await _mediator.Send(query);

        var notExistingGenres = genres?
            .Where(g => g.Value == null)
            .Select(g => g.Key)
            .ToArray();

        if (notExistingGenres == null || !notExistingGenres.Any()) return;

        var bulkRequest = new CreateGenresBulkRequest
        {
            GenreName = notExistingGenres
        };

        var command = new CreateGenresCommand(bulkRequest);
        await _mediator.Send(command);
    }
}