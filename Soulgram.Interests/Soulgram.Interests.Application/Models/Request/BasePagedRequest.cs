namespace Soulgram.Interests.Application.Models.Request;

public interface IPagedRequest
{
    int Page { get; init; }
    int Limit { get; init; }
}