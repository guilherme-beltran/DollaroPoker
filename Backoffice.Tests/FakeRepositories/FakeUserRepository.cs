using Backoffice.Application.UseCases.Users.Create;
using Backoffice.Domain.Entities;
using ZstdSharp.Unsafe;

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

    public static User? CreateValidUser() 
        => new(id: 1, 
               username: "Usuario padrão", 
               password: "senha123",
               jurisdiction: new Jurisdiction(id: 7,
                                              sknId: 82));
}
