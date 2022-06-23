using System.Linq.Expressions;
using MediatR;
using Soulgram.Interests.Application.Converters;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetInterestsQuery : IRequest<IEnumerable<InterestResponse>>
{
    public GetInterestsQuery(string[] interestsIds)
    {
        InterestsIds = interestsIds;
    }

    public string[] InterestsIds { get; }
}

internal class GetInterestsQueryHandler : IRequestHandler<GetInterestsQuery, IEnumerable<InterestResponse>>
{
    private readonly IRepository<Interest> _repository;

    public GetInterestsQueryHandler(IRepository<Interest> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<InterestResponse>> Handle(
        GetInterestsQuery request,
        CancellationToken cancellationToken)
    {
         Expression<Func<Interest, bool>>expression = request.InterestsIds.Any() 
             ? f => request.InterestsIds.Contains(f.Id) 
             : f => f.Id != null;

        return await _repository.FilterByAsync(
            expression,
            interest => interest.ToInterestResponse(),
            cancellationToken);
    }
}