using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces;

public interface IInterestsRepository : IRepository<Interest>
{
    Task AddUserToInterests(
        string userId,
        string[] interestsIds,
        CancellationToken cancellationToken);
}