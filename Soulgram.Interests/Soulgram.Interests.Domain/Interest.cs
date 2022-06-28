namespace Soulgram.Interests.Domain;

public record Interest
{
    public string? Id { get; set; }
    public InterestGroupType Type { get; init; }
    public string[] UsersIds { get; init; } = Array.Empty<string>();
}