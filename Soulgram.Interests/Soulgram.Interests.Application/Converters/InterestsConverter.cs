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
}