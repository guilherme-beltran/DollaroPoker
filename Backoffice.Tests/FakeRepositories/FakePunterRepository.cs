using Backoffice.Application.UseCases.Punters.Create;
using Backoffice.Domain.Entities;

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

    public static Punter CreatePunter()
        => Punter.Create(id: 1,
                         firstname: "Fulano",
                         middlename: "Ciclano",
                         lastname: "Beltrano",
                         username: "usuario",
                         password: "senha123",
                         jurisdiction: new Jurisdiction(id: 241,
                                                        sknId: 1));
}
