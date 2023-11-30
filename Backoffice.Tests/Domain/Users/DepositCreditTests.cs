using AutoFixture;
using Backoffice.Domain.Entities;
using Backoffice.Tests.FakeRepositories;

namespace Backoffice.Tests.Domain.Users;

[TestClass]
public class DepositCreditTests
{
    private Fixture fixture = new Fixture();
    private User user;
    private Punter punter;

    public DepositCreditTests()
    {
        
    }

    [TestMethod]
    public void Deve_parar_a_transferencia_e_enviar_notificacoes_de_erro_ao_depositar_valor_para_punter_nulo()
    {
        #region Arrange

        user = FakeUserRepository.CreateUser();

        #endregion

        #region Act

        user.DepositCredit(punter: null,
                           amount: 500);

        #endregion

        #region Assert

        Assert.IsTrue(user.Notifications.Count > 0);

        #endregion

    }

    [TestMethod]
    public void Deve_parar_a_transferencia_ao_depositar_valor_negativo()
    {
        #region Arrange

        user = FakeUserRepository.CreateUser();

        #endregion

        #region Act

        user.DepositCredit(punter: null,
                           amount: -500);

        #endregion

        #region Assert

        Assert.IsTrue(user.Notifications.Count > 0);

        #endregion

    }

    [TestMethod]
    public void Deve_parar_a_transferencia_ao_depositar_valor_maior_que_o_saldo_em_conta()
    {
        #region Arrange

        user = FakeUserRepository.CreateUser();

        #endregion

        #region Act

        user.DepositCredit(punter: null,
                           amount: 5001);

        #endregion

        #region Assert

        Assert.IsTrue(user.Notifications.Count > 0);

        #endregion

    }
}
