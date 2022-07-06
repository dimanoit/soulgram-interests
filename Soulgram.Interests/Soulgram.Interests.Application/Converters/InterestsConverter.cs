using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Converters;

public static class InterestsConverter
{
    public static Interest ToInterest(this InterestGroupType type)
    {
        var interest = new Interest
        {
            Type = type,
            UsersIds = Array.Empty<string>()
        };

        return interest;
    }

    public static InterestResponse ToInterestResponse(this Interest interest)
    {
        var response = new InterestResponse
        {
            Id = interest.Id!,
            Name = interest.Type.ToString()
        };

        return response;
    }
}