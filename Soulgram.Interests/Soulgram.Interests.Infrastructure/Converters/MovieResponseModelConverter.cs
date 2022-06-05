using Soulgram.Interests.Application;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Models;
using Soulgram.Interests.Infrastructure.Models.MainClientResponses;

namespace Soulgram.Interests.Infrastructure.Converters;

public static class MovieResponseModelConverter
{
    public static MovieSearchResponse ToMovieSearchResponse(this MovieResponseModel response)
    {
        var genres = response.Genre
            ?.Where(g => !string.IsNullOrEmpty(g))
            .Select(g => g.ToGenreResponse());

        var searchResponse = new MovieSearchResponse
        {
            ImdbId = response.ImdbId,
            Title = response.Title,
            BriefDescription = response.BriefDescription,
            ReleasedYear = response.ReleasedYear,
            ImgUrls = response.ImgUrls,
            Genres = genres
        };

        return searchResponse;
    }

    public static MovieSearchResponse ToMovieSearchResponse(this SearchMovieResult response)
    {
        throw new NotImplementedException();
    }

    private static GenreResponse ToGenreResponse(this string genreName)
    {
        // TODO get genre from DB and validate that we have this genre( mb better do it while insert movie to user)
        return new GenreResponse(genreName);
    }
}