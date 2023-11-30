using Backoffice.Domain.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace Backoffice.Application.UseCases.Punters.Unlock;

public class UnlockPunterCommand
    : Notifiable<Notification>, IRequest
{
    public string Username { get; set; }
    public string? Reason { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Username.Trim(), "UnlockPunterCommand.Username", "Please, provide punter username.")
            );
    }
}
