using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Converters;

public static class InterestsConverter
{
    public static Interest ToInterest(this string name)
    {
        return new Interest
        {
            Name = name,
            UsersIds = Array.Empty<string>()
        };
    }

    public static InterestResponse ToInterestResponse(this Interest interest)
    {
        return new InterestResponse
        {
            Id = interest.Id!,
            Name = interest.Name
        };
    }
}