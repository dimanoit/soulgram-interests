namespace Soulgram.Interests.Application.Models.Response;

public record GeneralInterestsResponse
{
    public string Id { get; init; } = null!;
    public string Name { get; init; } = null!;
}