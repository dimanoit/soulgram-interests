using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetInterestQuery : IRequest<InterestResponse>
{
    public GetInterestQuery(string interestId)
    {
        InterestId = interestId;
    }

    public string InterestId { get; }
}

internal class GetGeneralInterestQueryHandler : IRequestHandler<GetInterestQuery, InterestResponse>
{
    private readonly IRepository<Interest> _repository;

    public GetGeneralInterestQueryHandler(IRepository<Interest> repository)
    {
        _repository = repository;
    }

    public async Task<InterestResponse> Handle(
        GetInterestQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.FindOneAsync(
            f => f.Id == request.InterestId,
            p => new InterestResponse
            {
                Id = p.Id!,
                Name = p.Name
            },
            cancellationToken
        );
    }
}