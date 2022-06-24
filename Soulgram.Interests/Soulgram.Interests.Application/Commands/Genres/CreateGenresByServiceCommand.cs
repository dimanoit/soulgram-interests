using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request.Genres;

namespace Soulgram.Interests.Application.Commands.Genres;

public class CreateGenresByServiceCommand: IRequest { }

public class CreateGenresByServiceCommandHandler : IRequestHandler<CreateGenresByServiceCommand>
{
    private readonly IMovieService _movieService;
    private readonly IGenreRepository _genreRepository;
    private readonly IMediator _mediator;

    public CreateGenresByServiceCommandHandler(
        IMovieService movieService,
        IMediator mediator,
        IGenreRepository genreRepository)
    {
        _movieService = movieService;
        _mediator = mediator;
        _genreRepository = genreRepository;
    }

    public async Task<Unit> Handle(
        CreateGenresByServiceCommand request,
        CancellationToken cancellationToken)
    {
        var genres = await _movieService.GetGenresAsync(cancellationToken);
        var dbGenres = await _genreRepository.FilterByAsync(
            dbGenres => genres.Contains(dbGenres.Name),
            dbGenres => dbGenres.Name,
            cancellationToken);

        var notExistingGenres = genres
            .Except(dbGenres)
            .ToArray();
        
        if (!notExistingGenres.Any())
        {
            return Unit.Value;
        }
        
        var createGenresBulkRequest = new CreateGenresBulkRequest
        {
            GenreName = notExistingGenres,
            UserId = null
        };
        
        var createGenresCommand = new CreateGenresCommand(createGenresBulkRequest);

        await _mediator.Send(createGenresCommand, cancellationToken);
        
        return Unit.Value;
    }
}
