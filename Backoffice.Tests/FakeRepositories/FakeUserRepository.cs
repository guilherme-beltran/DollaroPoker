using Backoffice.Application.UseCases.Users.Create;

namespace Backoffice.Tests.FakeRepositories;

public class FakeUserRepository
{
    public static CreateUserCommand CreateUserRequestNullData()
    {
        return new CreateUserCommand
        {
            Username = null,
            Password = null,
            ConfirmPassword = null
        };
    }

    public static CreateUserCommand? CreateNullUserCommand()
    {
        return null;
    }

    public static CreateUserCommand? CreateValidUserCommand()
    {
        return new CreateUserCommand
        {
            Username = "teste",
            Password = "senha123",
            ConfirmPassword = "senha123"
        };
    }
}
