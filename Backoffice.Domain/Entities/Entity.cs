using Flunt.Notifications;

namespace Backoffice.Domain.Entities;

public abstract class Entity : Notifiable<Notification>
{
    public Guid Id { get; }
    public Entity()
        => Id = Guid.NewGuid();
}
