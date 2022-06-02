using Soulgram.Interests.Infrastructure.Models;

namespace Soulgram.Interests.Infrastructure.Filters;

public interface IMovieResponseFilter
{
    public IEnumerable<MovieResponseModel> Filter(IEnumerable<MovieResponseModel> models);
}