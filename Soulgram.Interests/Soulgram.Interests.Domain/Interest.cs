namespace Soulgram.Interests.Domain;

public record Interest
{
    public string? Id { get; set; }
    public string Name { get; init; } = null!;
    public string[] UsersIds { get; init; } = null!;
}