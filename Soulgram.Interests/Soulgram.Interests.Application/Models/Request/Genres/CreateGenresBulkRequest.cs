namespace Soulgram.Interests.Application.Models.Request.Genres;

public record CreateGenresBulkRequest
{
    public ICollection<string> GenreName { get; init; } = null!;
    public string? UserId { get; init; }
}