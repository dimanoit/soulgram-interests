namespace Soulgram.Interests.Domain;

public class UserInterests
{
    public string? Id { get; set; }
    public string Interest { get; init; } = null!;
    public string[] UsersIds { get; init; } = null!;
}