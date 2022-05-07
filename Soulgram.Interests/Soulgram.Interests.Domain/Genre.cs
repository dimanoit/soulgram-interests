namespace Soulgram.Interests.Domain;

public class Genre
{
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string[] UsersIds { get; set; } = null!;
}