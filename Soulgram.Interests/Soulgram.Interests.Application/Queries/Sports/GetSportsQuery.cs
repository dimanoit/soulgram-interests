using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;
using System.Linq.Expressions;

namespace Soulgram.Interests.Application.Queries.Sports;

public class GetSportsQuery : IRequest<ICollection<Sport>>
{
    public GetSportsQuery(ICollection<string> selectedSports)
    {
        SelectedSports = selectedSports;
    }

    public ICollection<string> SelectedSports { get; }
}

public class GetSportsQueryHandler : IRequestHandler<GetSportsQuery, ICollection<Sport>>
{
    private readonly IRepository<Sport> _repository;

    public GetSportsQueryHandler(IRepository<Sport> repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Sport>> Handle(GetSportsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Sport, bool>> filterExpression = request.SelectedSports != null
            ? g => request.SelectedSports.Contains(g.Name)
            : g => g.Id != null;

        return await _repository.FilterByAsync(
            filterExpression,
            x => x,
            cancellationToken);
    }
}