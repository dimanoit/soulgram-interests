namespace Soulgram.Interests.Application.Models.Response;

public class MovieSearchResponse
{
    public string ImdbId { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string? BriefDescription { get; init; }
    public int? ReleasedYear { get; init; }
    public IEnumerable<GenreResponse>? Genres { get; init; }
    public IEnumerable<string>? ImgUrls { get; init; }
}