using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Converters;

public static class GenreConverter
{
    public static Genre ToGenre(this CreateGenreRequest createGenreRequest)
    {
        var genre = new Genre
        {
            Name = createGenreRequest.GenreName,
            UsersIds = new[] {createGenreRequest.UserId!}
        };

        return genre;
    }
}