using Flunt.Notifications;
using System.Net;

namespace Backoffice.Domain.Shared;

public static class PunterErrors
{
    public static readonly Error InvalidRequest = new("PunterErrors.InvalidRequest", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");
    public static readonly Error NullReference = new("PunterErrors.NullReference", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");
    public static readonly Error ExceptionResult = new("PunterErrors.ExceptionResult", HttpStatusCode.InternalServerError);
    public static readonly Error AlreadyRegistered = new("PunterErrors.AlreadyRegistered", HttpStatusCode.Conflict, "Punter already registered.");
    public static readonly Error InvalidPassword = new("PunterErrors.InvalidPassword", HttpStatusCode.BadRequest, "Password don't match.");

    public static Error SendNotifications(IReadOnlyCollection<Notification>? notifications)
        => new("PunterErrors.InvalidRequest", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered", notifications);

    public static Error ReturnNullReference(string key)
       => new(key, HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");

    public static Error NotFound(string key, string search)
       => new(key, HttpStatusCode.BadRequest, $"{search} was not found.");

    public static Error Exception(string key, string message)
       => new(key, HttpStatusCode.InternalServerError, message);

    public static Error Failure(string key, string message)
       => new(key, HttpStatusCode.InternalServerError, message);

}
