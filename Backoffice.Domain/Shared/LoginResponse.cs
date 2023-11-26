using Flunt.Notifications;
using System.Net;

namespace Backoffice.Domain.Shared;

public class LoginResponse
{
    public LoginResponse(HttpStatusCode statusCode, int id, string username, string message)
    {
        StatusCode = statusCode;
        Id = id;
        Username = username;        
        Message = message;
    }

    public string Token { get; set; } = string.Empty;
    public int Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public HttpStatusCode StatusCode { get; private set; }
    public string Message { get; private set; }
}
