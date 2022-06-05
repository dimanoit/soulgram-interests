namespace Soulgram.Interests.Application.Models.Request;

public class CreateInterestBulkRequest
{
    public string[] Names { get; init; } = null!;
}