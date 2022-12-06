using MediatR;
using Soulgram.Interests.Application.Converters;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries.Interests;

public record GetInterestQuery(string InterestId) : IRequest<InterestResponse?>;
internal class GetGeneralInterestQueryHandler : IRequestHandler<GetInterestQuery, InterestResponse?>
{
    private readonly IRepository<Interest> _repository;

    public GetGeneralInterestQueryHandler(IRepository<Interest> repository)
    {
        _repository = repository;
    }

    public async Task<InterestResponse?> Handle(
        GetInterestQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.FindOneAsync(
            f => f.Id == request.InterestId,
            p => p.ToInterestResponse(),
            cancellationToken
        );
    }
}