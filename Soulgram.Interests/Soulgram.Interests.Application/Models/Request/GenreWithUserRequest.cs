namespace Soulgram.Interests.Application.Models.Request;

public record GenreWithUserRequest
{
    public string GenreName { get; init; } = null!;
    public string UserId { get; init; } = null!;
}