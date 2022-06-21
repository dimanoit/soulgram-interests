using MediatR;
using Soulgram.Interests.Application.Models.Request.Movies;

namespace Soulgram.Interests.Application.Commands.Movies;

public class AddMovieCommand : IRequest
{
    public AddMovieCommand(AddMovieRequest request)
    {
        Request = request;
    }

    public AddMovieRequest Request { get; }
    public string UserId { get; set; }
}