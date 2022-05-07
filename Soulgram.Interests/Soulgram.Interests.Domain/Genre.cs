namespace Soulgram.Interests.Domain;

public class Genre
{
    public string Name { get; set; } = null!;
    public ICollection<string>? UsersIds { get; set; }
}