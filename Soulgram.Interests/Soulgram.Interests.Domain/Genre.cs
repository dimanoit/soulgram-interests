namespace Soulgram.Interests.Domain;

public class Genre
{
    public string? Id { get; set; }
    public string Name { get; init; } = null!;
    public string[] UsersIds { get; set; } = null!;
}