using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetGeneralInterestsQuery : IRequest<UserInterests?>
{
    public GetGeneralInterestsQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}

internal class GetGeneralInterestsQueryHandler : IRequestHandler<GetGeneralInterestsQuery, UserInterests?>
{
    private readonly IRepository<UserInterests> _repository;

    public GetGeneralInterestsQueryHandler(IRepository<UserInterests> repository)
    {
        _repository = repository;
    }

    public async Task<UserInterests?> Handle(GetGeneralInterestsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.FindOneAsync(ui => ui.UserId == request.UserId, cancellationToken);
    }
}