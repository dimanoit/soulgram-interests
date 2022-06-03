using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces;

public interface IUserInterestsRepository : IRepository<UserInterests>
{
    Task AddUserToInterest(string userId, string interestId);
    Task AddUserToInterestBulk(string[] userId, string interestId);
    Task AddInterestsToOneUser(string userId, string[] interestsIds);
}