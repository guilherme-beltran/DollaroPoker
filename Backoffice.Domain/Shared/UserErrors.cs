using Flunt.Notifications;
using System.Net;

namespace Backoffice.Domain.Shared;

public static class UserErrors
{
    public static readonly Error InvalidRequest = new("UserError.InvalidRequest", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");
    public static readonly Error NullReference = new("UserError.NullReference", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");
    public static readonly Error ExceptionResult = new("UserError.ExceptionResult", HttpStatusCode.InternalServerError);
    public static readonly Error AlreadyRegistered = new("UserError.AlreadyRegistered", HttpStatusCode.Conflict, "User already registered.");
    public static readonly Error InvalidPassword = new("UserError.InvalidPassword", HttpStatusCode.BadRequest, "Password don't match.");

    public static Error SendNotifications(IReadOnlyCollection<Notification>? notifications)
        => new("UserError.InvalidRequest", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered", notifications);

    public static Error ReturnNullReference(string key)
       => new(key, HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");

    public static Error NotFound(string key, string search)
       => new(key, HttpStatusCode.BadRequest, $"{search} was not found.");

    public static Error Exception(string key, string message)
       => new(key, HttpStatusCode.InternalServerError, message);

}
