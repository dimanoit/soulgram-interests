namespace Soulgram.Interests.Application.Models.Request;

public record SearchMovieRequest : IPagedRequest
{
    public string Name { get; init; } = null!;

    public int Page { get; init; } = 1;
    public int Limit { get; init; } = 7;
}