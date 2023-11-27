using Backoffice.Domain.Extensions;
using Backoffice.Domain.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace Backoffice.Application.UseCases.Login;

public class LoginCommand : Notifiable<Notification>, IRequest
{
    public LoginCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }

    /// <summary>
    /// Username necessário para realizar login
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Password necessário para realizar login
    /// </summary>
    public string Password { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Password.Trim(), "LoginCommand.Password", "Invalid Password.")
                .IsGreaterOrEqualsThan(Password.Trim(), 6, "LoginCommand.Password", "Senha deve conter pelo menos 6 dígitos.")
                .IsLowerThan(Password.Trim(), 64, "LoginCommand.Password", "Senha deve ser menor que 64 caracteres.")
                .IsNotNullOrWhiteSpace(Username.Trim(), "LoginCommand.Username", "Email inválido.")
                .IsGreaterThan(Username.Trim(), 3, "LoginCommand.Username", "E-mail informado é inválido.")
                );

        if (Password is not null)
        {
            Password = Criptography.EncryptUsingSHA256(Password);
        }
    }
}
