using Backoffice.Application.UseCases.Punters.Create;

namespace Backoffice.Tests.FakeRepositories;

public class FakePunterRepository
{
    public static CreatePunterCommand CreatePunterRequestNullData()
    {
        return new CreatePunterCommand
        {
            FirstName = null,
            MiddleName = null,
            LastName = null,
            Username = null,
            Password = null,
            ConfirmPassword = null,
            Club = 1,
            Skin = 0
        };
    }

    public static CreatePunterCommand? CreateNullPunterCommand()
    {
        return null;
    }

    public static CreatePunterCommand CreateValidPunterCommand()
    {
        return new CreatePunterCommand
        {
            FirstName = "Primeiro",
            MiddleName = "Meio",
            LastName = "Ultimo",
            Username = "teste",
            Password = "senha123",
            ConfirmPassword = "senha123",
            Club = 1,
            Skin = 1
        };
    }
}
