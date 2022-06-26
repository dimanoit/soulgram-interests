using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Converters;

public static class MovieConverter
{
    public static MovieSearchResponse ToMovieSearchResponse(this Movie movie)
    {
        var genres = movie.GenresNames == null
            ? Enumerable.Empty<MovieGenreResponse>()
            : movie.GenresNames.Select(m => new MovieGenreResponse(m));

        var converted = new MovieSearchResponse
        {
            ImdbId = movie.ImdbId,
            Title = movie.Title,
            BriefDescription = movie.BriefDescription,
            ReleasedYear = movie.ReleasedYear,
            Genres = genres,
            ImgUrls = movie.ImgUrls
        };

        return converted;
    }

    public static AggregatedInterestItemValue ToAggregatedInterestItemValue(
        this MovieSearchResponse movie)
    {
        var itemValue = new AggregatedInterestItemValue
        {
            Name = movie.Title,
            ImgUrl = movie.ImgUrls?.FirstOrDefault()
        };

        return itemValue;
    }
}