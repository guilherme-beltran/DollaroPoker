using Flunt.Notifications;
using System.Net;

namespace Backoffice.Domain.Shared;

public sealed record Error(string Key, HttpStatusCode StatusCode, string? Message = null, IReadOnlyCollection<Notification>? Notifications = null)
{
    public static readonly Error None = new(Key: string.Empty, StatusCode: HttpStatusCode.OK, Message: string.Empty);

    public static implicit operator Response(Error error) => Response.Failure(error);
}