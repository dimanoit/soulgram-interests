using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Sports;

public class UploadSportsCommand : IRequest<ICollection<string>>
{
    public UploadSportsCommand(ICollection<string> sports)
    {
        Sports = sports;
    }

    public ICollection<string> Sports { get; }
}

public class UploadSportsCommandHandler : IRequestHandler<UploadSportsCommand, ICollection<string>>
{
    private readonly IRepository<Sport> _repository;

    public UploadSportsCommandHandler(IRepository<Sport> repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<string>> Handle(UploadSportsCommand request, CancellationToken cancellationToken)
    {
        var alreadyExistingItems = await _repository.FilterByAsync(
            x => request.Sports.Contains(x.Name),
            x => x.Name,
            cancellationToken);

        var itemsToCreate = request.Sports
            .Except(alreadyExistingItems)
            .Select(x => new Sport { Name = x })
            .ToArray();

        await _repository.InsertManyAsync(itemsToCreate, cancellationToken);

        return alreadyExistingItems;
    }
}