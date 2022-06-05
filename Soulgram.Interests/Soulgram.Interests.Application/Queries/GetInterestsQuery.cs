using System.Linq.Expressions;
using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries;

public class GetInterestsQuery : IRequest<IEnumerable<InterestResponse>>
{
    public GetInterestsQuery(string? userId)
    {
        UserId = userId;
    }

    public string? UserId { get; }
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
        Expression<Func<Interest, bool>> expression;
        if (string.IsNullOrEmpty(request.UserId))
        {
            expression = f => f.Id != null;
        }
        else
        {
            expression = f => f.UsersIds.Contains(request.UserId);
        }

        return await _repository.FilterByAsync(expression,
            f => new InterestResponse
            {
                Id = f.Id!,
                Name = f.Name
            });
    }
}