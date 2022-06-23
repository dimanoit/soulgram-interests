using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Converters;

public static class InterestsConverter
{
    public static Interest ToInterest(this string name)
    {
        var interest = new Interest
        {
            Name = name,
            UsersIds = Array.Empty<string>()
        };

        return interest;
    }

    public static InterestResponse ToInterestResponse(this Interest interest)
    {
        var response = new InterestResponse
        {
            Id = interest.Id!,
            Name = interest.Name
        };

        return response;
    }
}