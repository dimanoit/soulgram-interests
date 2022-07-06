using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Request.Interests;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Interests;

public record AddUserToInterestsCommand(
    AddInterestToUserRequest Request) : IRequest;

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

    public async Task<Unit> Handle(
        AddUserToInterestsCommand command,
        CancellationToken cancellationToken)
    {
        // TODO create model instead of passing 3 params
        await _interestsRepository.AddUserToInterests(
            command.Request.UserId,
            command.Request.InterestId,
            cancellationToken);

        var userFavorites = new UserFavorites
        {
            UserId = command.Request.UserId,
            Interests = new[]
            {
                new InterestsIds
                {
                    Type = command.Request.InterestType
                }
            }
        };

        await _userFavoritesService.UpsertFavorites(userFavorites, cancellationToken);

        return Unit.Value;
    }
}