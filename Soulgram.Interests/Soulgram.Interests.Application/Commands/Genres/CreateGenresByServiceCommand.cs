using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request.Genres;

namespace Soulgram.Interests.Application.Commands.Genres;

public class CreateGenresByServiceCommand: IRequest { }

public class CreateGenresByServiceCommandHandler : IRequestHandler<CreateGenresByServiceCommand>
{
    private readonly IMovieService _movieService;
    private readonly IMediator _mediator;

    public CreateGenresByServiceCommandHandler(IMovieService movieService, IMediator mediator)
    {
        _movieService = movieService;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(
        CreateGenresByServiceCommand request,
        CancellationToken cancellationToken)
    {
        var genres = await _movieService.GetGenresAsync(cancellationToken);

        var createGenresBulkRequest = new CreateGenresBulkRequest
        {
            GenreName = genres,
            UserId = null
        };
        
        var createGenresCommand = new CreateGenresCommand(createGenresBulkRequest);

        await _mediator.Send(createGenresCommand, cancellationToken);
        
        return Unit.Value;
    }
}
