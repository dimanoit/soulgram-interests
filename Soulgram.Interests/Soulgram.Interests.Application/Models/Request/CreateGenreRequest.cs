namespace Soulgram.Interests.Application.Models.Request;

public record CreateGenreRequest
{
    public string GenreName { get; init; } = null!;
    public string? UserId { get; init; }
}