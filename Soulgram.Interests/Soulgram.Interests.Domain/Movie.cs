namespace Soulgram.Interests.Domain;

public class Movie
{
    public string? Id { get; set; }
    public string ImdbId { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string? BriefDescription { get; init; }
    public int? ReleasedYear { get; init; }

    public IEnumerable<string>? GenresNames { get; init; }
    public IEnumerable<string>? ImgUrls { get; init; }

    
    public string[] UsersIds { get; set; } = Array.Empty<string>();
}