using MediatR;
using Soulgram.Interests.Application.Extensions;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetAllGeneralInterestsQuery : IRequest<IEnumerable<InterestType>> { }

internal class GetAllGeneralInterestsQueryHandler
    : IRequestHandler<GetAllGeneralInterestsQuery, IEnumerable<InterestType>>
{
    public Task<IEnumerable<InterestType>> Handle(
        GetAllGeneralInterestsQuery request,
        CancellationToken cancellationToken)
    {
        var result = EnumExtension.GetValues<InterestType>();
        return Task.FromResult(result);
    }
}