using System.Linq.Expressions;
using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetGeneralInterestsQuery : IRequest<IEnumerable<GeneralInterestsResponse>>
{
    public GetGeneralInterestsQuery(string? userId)
    {
        UserId = userId;
    }

    public string? UserId { get; }
}

internal class GetAllGeneralInterestsQueryHandler
    : IRequestHandler<GetGeneralInterestsQuery, IEnumerable<GeneralInterestsResponse>>
{
    private readonly IRepository<UserInterests> _repository;

    public GetAllGeneralInterestsQueryHandler(IRepository<UserInterests> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GeneralInterestsResponse>> Handle(
        GetGeneralInterestsQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<UserInterests, bool>> expression;
        if (string.IsNullOrEmpty(request.UserId))
            expression = f => f.Id != null;
        else
            expression = f => f.UsersIds.Contains(request.UserId);

        return await _repository.FilterByAsync(expression,
            f => new GeneralInterestsResponse
            {
                Id = f.Id!,
                Name = f.Interest
            });
    }
}