using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces;

public interface IUserInterestsRepository : IRepository<UserInterests>
{
    Task AddInterestToUserInterests(string userId, InterestType[] interest);
}