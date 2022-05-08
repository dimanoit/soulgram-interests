namespace Soulgram.Interests.Application;

public record GenreWithUserRequest
{
    public string[] GenresNames { get; init; } = null!;
    public string UserId { get; init; } = null!;
}