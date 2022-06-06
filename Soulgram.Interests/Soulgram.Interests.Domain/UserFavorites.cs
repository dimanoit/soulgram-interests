namespace Soulgram.Interests.Domain;

public record UserFavorites
{
    public string UserId { get; init; } = null!;
    public ICollection<Genre>? Genres { get; init; }
    public ICollection<Interest>? Interests { get; init; }
}