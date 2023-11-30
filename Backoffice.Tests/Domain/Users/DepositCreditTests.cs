using Backoffice.Domain.Entities;
using Backoffice.Tests.FakeRepositories;

namespace Backoffice.Tests.Domain.Users;

[TestClass]
public class DepositCreditTests
{
    private User user;
    private Punter punter;

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

    [TestMethod]
    public void Deve_realizar_a_transferencia_mantendo_saldo_do_admin_igual_a_4500()
    {
        #region Arrange

        user = FakeUserRepository.CreateUser();
        punter = FakePunterRepository.CreatePunter();

        #endregion

        #region Act

        user.DepositCredit(punter: punter,
                           amount: 500);

        #endregion

        #region Assert

        Assert.AreEqual(4500, user.Credit);

        #endregion

    }

    [TestMethod]
    public void Deve_realizar_a_transferencia_mantendo_saldo_do_punter_igual_a_5500()
    {
        #region Arrange

        user = FakeUserRepository.CreateUser();
        punter = FakePunterRepository.CreatePunter();

        #endregion

        #region Act

        user.DepositCredit(punter: punter,
                           amount: 500);

        #endregion

        #region Assert

        Assert.AreEqual(5500, punter.Credit);

        #endregion

    }
}
