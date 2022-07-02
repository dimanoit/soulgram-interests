namespace Soulgram.Interests.Domain;

public record InterestsIds
{
    public InterestGroupType Type { get; init; } = InterestGroupType.Others;
    public string[] Ids { get; set; } = Array.Empty<string>();
}