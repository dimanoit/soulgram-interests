using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Interests.Handlers;

internal class AddUserToInterestsCommandHandler : IRequestHandler<AddUserToInterestsCommand>
{
    private readonly IInterestsRepository _interestsRepository;
    private readonly IUserFavoritesRepository _userFavoritesRepository;

    public AddUserToInterestsCommandHandler(
        IInterestsRepository interestsRepository,
        IUserFavoritesRepository userFavoritesRepository)
    {
        _interestsRepository = interestsRepository;
        _userFavoritesRepository = userFavoritesRepository;
    }

    public async Task<Unit> Handle(AddUserToInterestsCommand request, CancellationToken cancellationToken)
    {
        var userFavorites = new UserFavorites
        {
            UserId = request.UserId,
            InterestsIds = request.InterestsIds
        };

        var favoriteId = await _userFavoritesRepository.GetId(request.UserId, cancellationToken);

        if (string.IsNullOrEmpty(favoriteId))
            await _userFavoritesRepository.InsertOneAsync(userFavorites, cancellationToken);
        else await _userFavoritesRepository.PushAsync(userFavorites, cancellationToken);

        await _interestsRepository
            .AddUserToInterests(request.UserId, request.InterestsIds, cancellationToken);

        return Unit.Value;
    }
}