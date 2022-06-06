using Soulgram.Interests.Application;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Models;
using Soulgram.Interests.Infrastructure.Models.MainClientResponses;

namespace Soulgram.Interests.Infrastructure.Converters;

public static class MovieResponseModelConverter
{
    public static MovieSearchResponse? ToMovieSearchResponse(this MovieResponseModel? response)
    {
        if (response == null) return null;

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

    public static MovieSearchResponse? ToMovieSearchResponse(this SearchMovieResult? response)
    {
        if (response == null) return null;

        var genres = response.GenreAggregated?.Genres?.Select(g => new GenreResponse(g.Text));
        var images = new[] {response.PrimaryImage?.Url};

        var converted = new MovieSearchResponse
        {
            ImdbId = response.Id,
            Title = response.TitleText.Text,
            ReleasedYear = response?.ReleaseDate?.Year,
            BriefDescription = response?.Plot?.PlotText?.PlainText,
            ImgUrls = images!,
            Genres = genres
        };

        return converted;
    }

    private static GenreResponse ToGenreResponse(this string genreName)
    {
        // TODO get genre from DB and validate that we have this genre( mb better do it while insert movie to user)
        return new GenreResponse(genreName);
    }
}