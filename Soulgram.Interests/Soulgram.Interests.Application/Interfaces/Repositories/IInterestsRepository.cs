using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces.Repositories;

public interface IInterestsRepository : IRepository<Interest>
{
    Task AddUserToInterests(
        string userId,
        string interestId,
        CancellationToken cancellationToken);
}