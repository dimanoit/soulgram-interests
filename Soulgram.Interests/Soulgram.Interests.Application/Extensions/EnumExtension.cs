namespace Soulgram.Interests.Application.Extensions;

public static class EnumExtension
{
    public static IEnumerable<T> GetValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
}