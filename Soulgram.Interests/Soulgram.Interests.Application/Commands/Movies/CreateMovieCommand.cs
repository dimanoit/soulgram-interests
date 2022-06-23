using MediatR;
using Soulgram.Interests.Application.Models.Request.Movies;

namespace Soulgram.Interests.Application.Commands.Movies;

public class CreateMovieCommand : IRequest
{
    public CreateMovieCommand(AddMovieRequest request)
    {
        Request = request;
    }

    public AddMovieRequest Request { get; }
    public string? UserId { get; set; }
}