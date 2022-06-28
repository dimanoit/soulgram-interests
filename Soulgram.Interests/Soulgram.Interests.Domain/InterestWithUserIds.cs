namespace Soulgram.Interests.Domain;

public record InterestWithUserIds
{
    public InterestGroupType Type { get; init; } = InterestGroupType.Others;
    public string[] UsersIds { get; set; } = Array.Empty<string>();
}