using MediatR;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands;

public class AddUserToGenresCommand: IRequest
{
    public AddUserToGenresCommand(GenresWithUserRequest request)
    {
        Request = request;
    }

    public GenresWithUserRequest Request { get; }
}


internal class AddUserToGenresCommandHandler : IRequestHandler<AddUserToGenresCommand>
{
    private readonly IMediator _mediator;

    public AddUserToGenresCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<Unit> Handle(AddUserToGenresCommand command, CancellationToken cancellationToken)
    {
        foreach (var genre in command.Request.GenresNames)
        {
            var request = new GenreWithUserRequest();
            var command = new AddUserToGenreCommand()
        }
    }
}