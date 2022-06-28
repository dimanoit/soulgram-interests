using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Interests;

public record AddUserToInterestsCommand(string UserId, string[] InterestsIds) : IRequest;
internal class AddUserToInterestsCommandHandler : IRequestHandler<AddUserToInterestsCommand>
{
    private readonly IInterestsRepository _interestsRepository;
    private readonly IUserFavoritesService _userFavoritesService;

    public AddUserToInterestsCommandHandler(
        IInterestsRepository interestsRepository,
        IUserFavoritesService userFavoritesService)
    {
        _interestsRepository = interestsRepository;
        _userFavoritesService = userFavoritesService;
    }

    public async Task<Unit> Handle(AddUserToInterestsCommand request, CancellationToken cancellationToken)
    {
        await UpsertUserFavorites(request, cancellationToken);

        await _interestsRepository
            .AddUserToInterests(request.UserId, request.InterestsIds, cancellationToken);

        return Unit.Value;
    }

    private async Task UpsertUserFavorites(AddUserToInterestsCommand request, CancellationToken cancellationToken)
    {
        var userFavorites = new UserFavorites
        {
            UserId = request.UserId,
            InterestsIds = request.InterestsIds
        };

        await _userFavoritesService.UpsertFavorites(userFavorites, cancellationToken);
    }
}