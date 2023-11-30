using Backoffice.Application.Interfaces.Punters;
using Backoffice.Domain.Interfaces.Repositories.Cache;
using Backoffice.Domain.Shared;

namespace Backoffice.Application.UseCases.Punters.Unlock;

internal sealed class UnlockPunterHandler : IUnlockPunterHandler
{
    private readonly ICachePunterRepository _cachePunterRepository;

    public UnlockPunterHandler(ICachePunterRepository cachePunterRepository)
    {
        _cachePunterRepository = cachePunterRepository;
    }

    public async Task<Response> Handle(UnlockPunterCommand request, CancellationToken cancellationToken)
    {
        #region Validate request

        request.Clear();

        try
        {
            request.Validate();
            if (!request.IsValid)
                return PunterErrors.SendNotifications(notifications: request.Notifications);
        }
        catch (NullReferenceException)
        {
            return PunterErrors.ReturnNullReference("LockPunterHandler");
        }
        catch (Exception)
        {
            return PunterErrors.ExceptionResult;
        }

        #endregion

        #region Get punter 

        var punter = await _cachePunterRepository.GetByUsernameAsync(username: request.Username);
        if (punter is null)
            return PunterErrors.NotFound("UnlockPunterHandler.punter", search: $"Punter {request.Username}");

        #endregion

        #region Unlock 

        punter.Clear();

        try
        {
            punter.Unlock(reason: request.Reason);
            if (!punter.IsValid)
                return PunterErrors.SendNotifications(notifications: punter.Notifications);

            var locked = await _cachePunterRepository.UnlockAsync(punter, cancellationToken);
            if (!locked)
            {
                return PunterErrors.Failure("UnlockPunterHandler.UnlockAsync", "There was a failure. Please try again later.");
            }
                
        }
        catch (Exception ex)
        {
            return PunterErrors.Exception("UnlockPunterHandler.UnlockAsync", $"{ex.Message}");
        }

        #endregion


        return Response.Sucess($"Punter {request.Username} unlocked with success.");
    }
}
