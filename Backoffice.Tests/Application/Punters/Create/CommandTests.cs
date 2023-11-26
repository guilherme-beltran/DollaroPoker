using AutoFixture;
using Backoffice.Application.Interfaces.Punters;
using Backoffice.Application.UseCases.Punters.Create;
using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Interfaces.UnitOfWork;
using Backoffice.Domain.Shared;
using Backoffice.Tests.FakeRepositories;
using Moq;
using System.Net;

namespace Backoffice.Tests.Application.Punters.Create;

[TestClass]
public class CommandTests
{
    private CreatePunterCommand? command;
    private ICreatePunterHandler handler;
    private readonly Mock<IPunterRepository> _punterRepository;
    private readonly Mock<IJurisdictionRepository> _jurisdictionRepository;
    private readonly Mock<ISequenceRepository> _sequenceRepository;
    private readonly Mock<IUnitOfWork> _uow;
    private Fixture fixture = new Fixture();

    public CommandTests()
    {
        _punterRepository = new Mock<IPunterRepository>();
        _jurisdictionRepository = new Mock<IJurisdictionRepository>();
        _sequenceRepository = new Mock<ISequenceRepository>();
        _uow = new Mock<IUnitOfWork>();

        handler = new CreatePunterHandler(_punterRepository.Object, _jurisdictionRepository.Object, _sequenceRepository.Object, _uow.Object);
    }

    [TestMethod]
    public void Deve_retornar_NullReferenceResponse_ao_informar_request_nulo()
    {
        #region Arrange

        command = FakePunterRepository.CreateNullPunterCommand();

        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual(typeof(Response), response.GetType());

        #endregion       

    }

    [TestMethod]
    public void Deve_retornar_NullReferenceResponse_ao_informar_request_com_dados_nulos()
    {
        #region Arrange

        command = FakePunterRepository.CreatePunterRequestNullData();

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(false);

        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual(HttpStatusCode.BadRequest, response.Error.StatusCode);
        //Assert.AreEqual(typeof(NullReferenceResponse), response.GetType());

        #endregion       

    }

    [TestMethod]
    [DataRow("f", "m", "l", "u", "p", "cp", 1, 0)]
    [DataRow("ff", "mm", "ll", "uu", "pp", "cp", 1, 0)]
    [DataRow("fff", "mmm", "lll", "uuu", "ppp", "cpp", 1, 0)]
    public void Deve_retornar_StatusCode_BadRequest_e_notificacoes_de_erro_ao_informar_request_com_menos_de_3_caracteres(string firstname, string middlename, string lastname, string username, string password, string confirmPassword, int club, int skin)
    {
        #region Arrange

        command = new CreatePunterCommand
        {
            FirstName = firstname,
            MiddleName = middlename,
            LastName = lastname,
            Username = username,
            Password = password,
            ConfirmPassword = confirmPassword,
            Club = club,
            Skin = skin
        };

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(false);

        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.IsTrue(response.Error.Notifications.Count > 0);

        #endregion       

    }

    [TestMethod]
    [DataRow("user")]
    [DataRow("usern")]
    [DataRow("userna")]
    [DataRow("userna")]
    [DataRow("usernam")]
    [DataRow("username")]
    [DataRow("username1")]
    [DataRow("username2")]
    [DataRow("username3")]
    [DataRow("username4")]
    [DataRow("username5")]
    [DataRow("username6")]
    [DataRow("username7")]
    [DataRow("username8")]
    [DataRow("username9")]
    [DataRow("username10")]
    public void Deve_retornar_StatusCode_OK_ao_informar_username_valido_na_requisicao(string username)
    {
        #region Arrange

        command = FakePunterRepository.CreateValidPunterCommand();
        command.Username = username;

        var sequence = fixture.Create<Sequence>();
        var jurisdiction = fixture.Create<Jurisdiction>();

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(false);

        _jurisdictionRepository.Setup(j => j.GetByIdAsync(command.Club)).ReturnsAsync(jurisdiction);

        _sequenceRepository.Setup(s => s.GetSequencePunterAsync()).ReturnsAsync(sequence);

        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual(HttpStatusCode.OK, response.Error.StatusCode);

        #endregion
    }

    [TestMethod]
    [DataRow("username10")]
    public void Deve_retornar_tipo_Response(string username)
    {
        #region Arrange

        command = FakePunterRepository.CreateValidPunterCommand();
        command.Username = username;

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(false);

        var sequence = fixture.Create<Sequence>();
        var jurisdiction = fixture.Create<Jurisdiction>();
        _jurisdictionRepository.Setup(j => j.GetByIdAsync(command.Club)).ReturnsAsync(jurisdiction);

        _sequenceRepository.Setup(s => s.GetSequencePunterAsync()).ReturnsAsync(sequence);


        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual(typeof(Response), response.GetType());

        #endregion
    }

    [TestMethod]
    [DataRow("use")]
    [DataRow("user")]
    [DataRow("usern")]
    [DataRow("userna")]
    [DataRow("userna")]
    [DataRow("usernam")]
    [DataRow("username")]
    [DataRow("username1")]
    [DataRow("username2")]
    [DataRow("username3")]
    [DataRow("username4")]
    [DataRow("username5")]
    [DataRow("username6")]
    [DataRow("username7")]
    [DataRow("username8")]
    [DataRow("username9")]
    [DataRow("username10")]
    public void Deve_retornar_tipo_StatusCode_OK_ao_informar_username_valido_na_requisicao(string username)
    {
        #region Arrange

        command = FakePunterRepository.CreateValidPunterCommand();
        command.Username = username;

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(false);
        var sequence = fixture.Create<Sequence>();
        var jurisdiction = fixture.Create<Jurisdiction>();
        _jurisdictionRepository.Setup(j => j.GetByIdAsync(command.Club)).ReturnsAsync(jurisdiction);

        _sequenceRepository.Setup(s => s.GetSequencePunterAsync()).ReturnsAsync(sequence);


        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual(typeof(Response), response.GetType());

        #endregion
    }

    [TestMethod]
    [DataRow("pas")]
    [DataRow("pass")]
    [DataRow("passw")]
    [DataRow("passwo")]
    [DataRow("passwor")]
    [DataRow("password")]
    [DataRow("password1")]
    [DataRow("password2")]
    [DataRow("password3")]
    [DataRow("password4")]
    [DataRow("password5")]
    [DataRow("password6")]
    [DataRow("password7")]
    [DataRow("password8")]
    [DataRow("password9")]
    [DataRow("password10")]
    public void Deve_retornar_BadRequest_ao_informar_ConfirmPassword_diferente_do_Password(string confirmPassword)
    {
        #region Arrange

        command = FakePunterRepository.CreateValidPunterCommand();
        command.ConfirmPassword = confirmPassword;

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(false);
        var sequence = fixture.Create<Sequence>();
        var jurisdiction = fixture.Create<Jurisdiction>();
        _jurisdictionRepository.Setup(j => j.GetByIdAsync(command.Club)).ReturnsAsync(jurisdiction);

        _sequenceRepository.Setup(s => s.GetSequencePunterAsync()).ReturnsAsync(sequence);


        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual(HttpStatusCode.BadRequest, response.Error.StatusCode);
        //Assert.AreEqual(typeof(InvalidRequest), response.GetType());

        #endregion
    }

    [TestMethod]
    [DataRow("senha123")]
    public void Deve_retornar_StatusCode_OK_ao_informar_ConfirmPassword_igual_ao_Password(string confirmPassword)
    {
        #region Arrange

        command = FakePunterRepository.CreateValidPunterCommand();
        command.ConfirmPassword = confirmPassword;

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(false);
        var sequence = fixture.Create<Sequence>();
        var jurisdiction = fixture.Create<Jurisdiction>();
        _jurisdictionRepository.Setup(j => j.GetByIdAsync(command.Club)).ReturnsAsync(jurisdiction);

        _sequenceRepository.Setup(s => s.GetSequencePunterAsync()).ReturnsAsync(sequence);


        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual(HttpStatusCode.OK, response.Error.StatusCode);
        //Assert.AreEqual(typeof(CreatedSuccessfully), response.GetType());

        #endregion
    }
}
