using Backoffice.Domain.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace Backoffice.Application.UseCases.Punters.Create;

public class CreatePunterCommand : Notifiable<Notification>, IRequest
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public int Club { get; set; }
    public int Skin { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Username.Trim(), "CreatePunterCommand.Username", "Please, provide your username.")
            .IsNotNullOrWhiteSpace(Password.Trim(), "CreatePunterCommand.Password", "Please, provide your Password.")
            .IsNotNullOrWhiteSpace(ConfirmPassword.Trim(), "CreatePunterCommand.ConfirmPassword", "Please, provide your ConfirmPassword.")
            .IsNotNullOrWhiteSpace(FirstName.Trim(), "CreatePunterCommand.Fullname", "Please, provide your Fullname.")
            .IsNotNullOrWhiteSpace(MiddleName.Trim(), "CreatePunterCommand.MiddleName", "Please, provide your MiddleName.")
            .IsNotNullOrWhiteSpace(LastName.Trim(), "CreatePunterCommand.LastName", "Please, provide your LastName.")
            .IsNotNull(Skin, "CreatePunterCommand.Skin", "Please, provide your Skin.")
            .IsNotNull(Club, "CreatePunterCommand.Club", "Please, provide your Club.")
            .IsGreaterOrEqualsThan(Username.Trim(), 3, "CreatePunterCommand.Username", "The Username must contain at least 3 characters.")
            .IsGreaterOrEqualsThan(Password.Trim(), 3, "CreatePunterCommand.Password", "The Password must contain at least 3 characters.")
            .IsGreaterOrEqualsThan(ConfirmPassword.Trim(), 3, "CreatePunterCommand.ConfirmPassword", "The ConfirmPassword must contain at least 3 characters.")
            .IsLowerThan(Username.Trim(), 50, "CreatePunterCommand.Username", "The Username must contain a maximum of 50 characters")
            .IsLowerThan(Password.Trim(), 50, "CreatePunterCommand.Password", "The Password must contain a maximum of 50 characters")
            .IsLowerThan(ConfirmPassword.Trim(), 50, "CreatePunterCommand.ConfirmPassword", "The ConfirmPassword must contain a maximum of 50 characters")
            .IsLowerThan(FirstName.Trim(), 100, "CreatePunterCommand.FirstName", "The FirstName must contain a maximum of 100 characters")
            .IsLowerThan(MiddleName.Trim(), 100, "CreatePunterCommand.MiddleName", "The MiddleName must contain a maximum of 100 characters")
            .IsLowerThan(LastName.Trim(), 100, "CreatePunterCommand.LastName", "The LastName must contain a maximum of 100 characters")
            .AreEquals(Password.Trim(), ConfirmPassword.Trim(), "CreatePunterCommand.ConfirmPassword", "The password and password confirmation don't match.")
            );
    }
}
