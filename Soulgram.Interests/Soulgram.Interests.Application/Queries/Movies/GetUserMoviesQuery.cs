using MediatR;
using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Application.Queries.Movies;

public class GetUserMoviesQuery : IRequest<ICollection<MovieSearchResponse>?>
{
    public GetUserMoviesQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}