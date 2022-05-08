using MediatR;
using Soulgram.Interests.Application.Models.Request;

namespace Soulgram.Interests.Application.Commands;

public class AddUserToGenresCommand : IRequest
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

    public async Task<Unit> Handle(AddUserToGenresCommand command, CancellationToken cancellationToken)
    {
        var tasks = command.Request
            .GenresNames
            .Distinct()
            .Select(async n => await AddUserToGenreCommandTask(command.Request.UserId, n));

        await Task.WhenAll(tasks);

        return Unit.Value;
    }

    private Task<Unit> AddUserToGenreCommandTask(string userId, string genreName)
    {
        var request = new GenreWithUserRequest
        {
            GenreName = genreName,
            UserId = userId
        };

        return _mediator.Send(new AddUserToGenreCommand(request));
    }
}