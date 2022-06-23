namespace Soulgram.Interests.Application.Models.Request.Movies;

public record AddMovieRequest
{
    public string ImdbId { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string? BriefDescription { get; init; }
    public int? ReleasedYear { get; init; }

    public IEnumerable<string>? Genres { get; init; }
    public IEnumerable<string>? ImgUrls { get; init; }
}