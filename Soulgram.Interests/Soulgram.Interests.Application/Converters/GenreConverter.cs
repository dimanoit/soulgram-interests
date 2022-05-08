using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Converters;

public static class GenreConverter
{
    public static Genre ToGenre(this GenreWithUserRequest genreWithUserRequest)
    {
        return new Genre
        {
            Name = genreWithUserRequest.GenreName,
            UsersIds = new[] {genreWithUserRequest.UserId}
        };
    }
}