using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetGeneralInterestQuery : IRequest<GeneralInterestsResponse>
{
    public GetGeneralInterestQuery(string interestId)
    {
        InterestId = interestId;
    }

    public string InterestId { get; }
}

internal class GetGeneralInterestQueryHandler
    : IRequestHandler<GetGeneralInterestQuery, GeneralInterestsResponse>
{
    private readonly IRepository<Domain.Interest> _repository;

    public GetGeneralInterestQueryHandler(IRepository<Domain.Interest> repository)
    {
        _repository = repository;
    }

    public async Task<GeneralInterestsResponse> Handle(
        GetGeneralInterestQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.FindOneAsync(
            f => f.Id == request.InterestId,
            p => new GeneralInterestsResponse
            {
                Id = p.Id!,
                Name = p.Name
            },
            cancellationToken
        );
    }
}