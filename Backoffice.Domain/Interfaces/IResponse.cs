using Flunt.Notifications;
using System.Net;

namespace Backoffice.Domain.Interfaces;

public interface IResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public IReadOnlyCollection<Notification> Notifications { get; set; }
}
