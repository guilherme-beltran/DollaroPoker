using Backoffice.Domain.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace Backoffice.Application.UseCases.Users.Create;

public class CreateUserCommand : Notifiable<Notification>, IRequest
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public int Club { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Username.Trim(), "CreateUserCommand.Username", "Please, provide your username.")
            .IsNotNullOrWhiteSpace(Password.Trim(), "CreateUserCommand.Password", "Please, provide your Password.")
            .IsNotNullOrWhiteSpace(ConfirmPassword.Trim(), "CreateUserCommand.ConfirmPassword", "Please, provide your ConfirmPassword.")
            .IsNotNullOrWhiteSpace(Name.Trim(), "CreateUserCommand.Fullname", "Please, provide your Fullname.")
            .IsNotNull(Club, "CreateUserCommand.Club", "Please, provide your Club.")
            .IsGreaterOrEqualsThan(Username.Trim(), 3, "CreateUserCommand.Username", "The Username must contain at least 3 characters.")
            .IsGreaterOrEqualsThan(Password.Trim(), 3, "CreateUserCommand.Password", "The Password must contain at least 3 characters.")
            .IsGreaterOrEqualsThan(ConfirmPassword.Trim(), 3, "CreateUserCommand.ConfirmPassword", "The ConfirmPassword must contain at least 3 characters.")
            .IsGreaterOrEqualsThan(Name.Trim(), 3, "CreateUserCommand.Fullname", "The Fullname must contain at least 3 characters.")
            .IsLowerThan(Username.Trim(), 50, "CreateUserCommand.Username", "The Username must contain a maximum of 50 characters")
            .IsLowerThan(Password.Trim(), 50, "CreateUserCommand.Password", "The Password must contain a maximum of 50 characters")
            .IsLowerThan(ConfirmPassword.Trim(), 50, "CreateUserCommand.ConfirmPassword", "The ConfirmPassword must contain a maximum of 50 characters")
            .IsLowerThan(Name.Trim(), 100, "CreateUserCommand.FirstName", "The FirstName must contain a maximum of 100 characters")
            .AreEquals(Password.Trim(), ConfirmPassword.Trim(), "CreateUserCommand.ConfirmPassword", "The password and password confirmation don't match.")
            );
    }
}
