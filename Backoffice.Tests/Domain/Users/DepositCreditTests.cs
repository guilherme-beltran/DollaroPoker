using AutoFixture;
using Backoffice.Domain.Entities;
using Backoffice.Tests.FakeRepositories;

namespace Backoffice.Tests.Domain.Users;

[TestClass]
public class DepositCreditTests
{
    private Fixture fixture = new Fixture();
    private User mockUser;
    private User user;

    public DepositCreditTests()
    {
        user = FakeUserRepository.CreateValidUser();
        mockUser = fixture.Create<User>();
    }

    [TestMethod]
    public void Deve_falhar()
    {
        Assert.Fail();
    }
}
