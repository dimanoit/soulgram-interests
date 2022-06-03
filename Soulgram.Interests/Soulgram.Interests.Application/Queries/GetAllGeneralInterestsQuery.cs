using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetAllGeneralInterestsQuery : IRequest<IEnumerable<GeneralInterestsResponse>>
{
}

internal class GetAllGeneralInterestsQueryHandler
    : IRequestHandler<GetAllGeneralInterestsQuery, IEnumerable<GeneralInterestsResponse>>
{
    private readonly IRepository<UserInterests> _repository;

    public GetAllGeneralInterestsQueryHandler(IRepository<UserInterests> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GeneralInterestsResponse>> Handle(
        GetAllGeneralInterestsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.FilterByAsync(
            f => f.Id != null,
            f => new GeneralInterestsResponse
            {
                Id = f.Id!,
                Name = f.Interest
            });
    }
}