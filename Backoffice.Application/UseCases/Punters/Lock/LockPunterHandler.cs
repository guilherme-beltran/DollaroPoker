using Backoffice.Application.Interfaces.Punters;
using Backoffice.Domain.Interfaces.Repositories.Cache;
using Backoffice.Domain.Shared;

namespace Backoffice.Application.UseCases.Punters.Lock;

internal sealed class LockPunterHandler : ILockPunterHandler
{
    private readonly ICachePunterRepository _cachePunterRepository;

    public LockPunterHandler(ICachePunterRepository cachePunterRepository)
    {
        _cachePunterRepository = cachePunterRepository;
    }

    public async Task<Response> Handle(LockPunterCommand request, CancellationToken cancellationToken)
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
            return PunterErrors.NotFound("LockPunterHandler.punter", search: $"Punter {request.Username}");

        #endregion

        #region Lock 

        punter.Clear();

        try
        {
            punter.Lock(reason: request.Reason);
            if (!punter.IsValid)
                return PunterErrors.SendNotifications(notifications: punter.Notifications);

            var locked = await _cachePunterRepository.LockAsync(punter, cancellationToken);
            if (!locked)
            {
                return PunterErrors.Failure("LockPunterHandler.LockAsync", "There was a failure. Please try again later.");
            }
                
        }
        catch (Exception ex)
        {
            return PunterErrors.Exception("LockPunterHandler.LockAsync", $"{ex.Message}");
        }

        #endregion


        return Response.Sucess($"Punter {request.Username} locked with success.");
    }
}
