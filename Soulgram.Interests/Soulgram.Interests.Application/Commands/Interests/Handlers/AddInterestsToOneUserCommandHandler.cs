using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Interests.Handlers;

internal class AddInterestsToOneUserCommandHandler : IRequestHandler<AddInterestsToOneUserCommand>
{
    private readonly IInterestsRepository _interestsRepository;
    private readonly IUserFavoritesRepository _userFavoritesRepository;
    
    public AddInterestsToOneUserCommandHandler(
        IInterestsRepository interestsRepository,
        IUserFavoritesRepository userFavoritesRepository)
    {
        _interestsRepository = interestsRepository;
        _userFavoritesRepository = userFavoritesRepository;
    }

    public async Task<Unit> Handle(AddInterestsToOneUserCommand request, CancellationToken cancellationToken)
    {
        await _userFavoritesRepository
            .AddOrCreateFavorites<Interest>(request.UserId, request.InterestsIds,cancellationToken);
        
        await _interestsRepository
            .AddInterestsToOneUser(request.UserId, request.InterestsIds, cancellationToken);
        
        return Unit.Value;
    }
}