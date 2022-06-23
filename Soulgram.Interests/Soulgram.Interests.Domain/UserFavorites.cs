namespace Soulgram.Interests.Domain;

public record UserFavorites
{
    public string Id { get; set; }
    public string UserId { get; init; } = null!;
    public string[] InterestsIds { get; set; } = Array.Empty<string>();
    public string[] GenresIds { get; set; } = Array.Empty<string>();
    public string[] MoviesIds { get; set; } = Array.Empty<string>();
}