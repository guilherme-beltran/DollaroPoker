using Backoffice.Application.Interfaces.Punters;
using Backoffice.Application.UseCases.Punters.Create;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Interfaces.UnitOfWork;
using Backoffice.Tests.FakeRepositories;
using Moq;
using System.Net;

namespace Backoffice.Tests.Application.Punters.Create;


[TestClass]
public class AlreadyRegisteredTests
{
    private CreatePunterCommand? command;
    private ICreatePunterHandler handler;
    private readonly Mock<IPunterRepository> _punterRepository;
    private readonly Mock<IJurisdictionRepository> _jurisdictionRepository;
    private readonly Mock<ISequenceRepository> _sequenceRepository;
    private readonly Mock<IUnitOfWork> _uow;

    public AlreadyRegisteredTests()
    {
        _punterRepository = new Mock<IPunterRepository>();
        _jurisdictionRepository = new Mock<IJurisdictionRepository>();
        _sequenceRepository = new Mock<ISequenceRepository>();
        _uow = new Mock<IUnitOfWork>();

        handler = new CreatePunterHandler(_punterRepository.Object,
                                          _jurisdictionRepository.Object,
                                          _sequenceRepository.Object,
                                          _uow.Object);
    }

    [TestMethod]
    public void Deve_retornar_response_AlreadyRegistered_ao_informar_usuario_ja_cadastrado()
    {
        #region Arrange

        command = FakePunterRepository.CreateValidPunterCommand();

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(true);

        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual("Punter already registered.", response.Error.Message);

        #endregion       

    }

    [TestMethod]
    public void Deve_retornar_StatusCode_Conflict_ao_informar_usuario_ja_cadastrado()
    {
        #region Arrange

        command = FakePunterRepository.CreateValidPunterCommand();

        _punterRepository.Setup(ur => ur.IsRegisteredAsync(command.Username)).ReturnsAsync(true);

        #endregion

        #region Act

        var response = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        #endregion

        #region Assert

        Assert.AreEqual(HttpStatusCode.Conflict, response.Error.StatusCode);

        #endregion       

    }
}
