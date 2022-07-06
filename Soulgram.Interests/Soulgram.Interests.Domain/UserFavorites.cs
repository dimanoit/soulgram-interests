namespace Soulgram.Interests.Domain;

public record UserFavorites
{
    public string? Id { get; set; }
    public string UserId { get; init; } = null!;
    public InterestsIds[] Interests { get; init; } = null!;
}