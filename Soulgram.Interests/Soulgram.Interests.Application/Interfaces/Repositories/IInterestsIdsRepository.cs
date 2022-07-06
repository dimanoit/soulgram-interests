using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces.Repositories;

public interface IInterestsIdsRepository
{
    Task AddUserIdToGenre(InterestGroupType type, string id);
}