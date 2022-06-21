namespace Soulgram.Interests.Application.Models.Request;

public record GenresWithUserRequest
{
    public string[] GenresNames { get; init; } = null!;
    public string UserId { get; init; } = null!;
}