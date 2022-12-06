namespace Soulgram.Interests.Domain;

public class Sport
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string[] UsersIds { get; set; } = Array.Empty<string>();
}