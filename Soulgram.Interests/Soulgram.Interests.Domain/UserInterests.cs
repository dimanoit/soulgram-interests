namespace Soulgram.Interests.Domain;

public class UserInterests
{
    public string? Id { get; set; }

    public string UserId { get; init; } = null!;

    public InterestType[] Interests { get; init; } = null!;
}