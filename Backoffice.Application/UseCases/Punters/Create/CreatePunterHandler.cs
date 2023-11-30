using Backoffice.Application.Interfaces.Punters;
using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Interfaces.Repositories.Cache;
using Backoffice.Domain.Interfaces.UnitOfWork;
using Backoffice.Domain.Shared;
using MySql.Data.MySqlClient;

namespace Backoffice.Application.UseCases.Punters.Create;

internal class CreatePunterHandler : ICreatePunterHandler
{
    private readonly ICachePunterRepository _cachePunterRepository;
    private readonly IJurisdictionRepository _jurisdictionRepository;
    private readonly ISequenceRepository _sequenceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePunterHandler(ICachePunterRepository cachePunterRepository,
                               IJurisdictionRepository jurisdictionRepository,
                               ISequenceRepository sequenceRepository,
                               IUnitOfWork unitOfWork)
    {
        _cachePunterRepository = cachePunterRepository;
        _jurisdictionRepository = jurisdictionRepository;
        _sequenceRepository = sequenceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(CreatePunterCommand request, CancellationToken cancellationToken)
    {
        #region Validate request

        try
        {
            request.Validate();
            if (!request.IsValid)
                return PunterErrors.SendNotifications(notifications: request.Notifications);
        }
        catch (NullReferenceException)
        {
            return PunterErrors.ReturnNullReference("CreatePunterHandler");
        }
        catch (Exception)
        {
            return PunterErrors.ExceptionResult;
        }

        #endregion

        #region Checks if user already registered

        try
        {
            var isRegistered = await _cachePunterRepository.IsRegisteredAsync(request.Username!);
            if (isRegistered)
                return Response.Failure(PunterErrors.AlreadyRegistered);
        }
        catch (Exception)
        {
            return PunterErrors.ExceptionResult;
        }

        #endregion

        #region Get Jurisdiction

        Jurisdiction jurisdiction;
        try
        {
            jurisdiction = await _jurisdictionRepository.GetByIdAsync(id: request.Club);
            if (jurisdiction is null)
                return PunterErrors.NotFound("CreatePunterHandler.jurisdiction", search: $"Id {request.Club}");
        }
        catch (Exception)
        {
            return PunterErrors.ExceptionResult;
        }

        #endregion

        #region Get sequence

        Sequence sequence;
        try
        {
            sequence = await _sequenceRepository.GetSequencePunterAsync();
            if (sequence is null)
                return PunterErrors.NotFound("CreatePunterHandler.sequence", search: $"Sequence Id Punter");
        }
        catch (Exception)
        {
            return PunterErrors.ExceptionResult;
        }

        #endregion

        #region Create punter

        sequence!.UpdateSequence();

        var punter = Punter.Create(id: sequence.NextSequence,
                                   firstname: request.FirstName,
                                   middlename: request.MiddleName,
                                   lastname: request.LastName,
                                   username: request.Username,
                                   password: request.Password,
                                   jurisdiction: jurisdiction!);

        punter.Encrypt();


        try
        {
            _unitOfWork.BeginTransaction();

            await _cachePunterRepository.Insert(punter: punter, cancellationToken: cancellationToken);

            await _unitOfWork.Commit(cancellationToken: cancellationToken);

            return Response.Sucess($"{request.Username} created successfully.");
        }
        catch (MySqlException ex)
        {
            _unitOfWork.Rollback();
            return PunterErrors.Exception("CreatePunterHandler.Commit()", $"{ex.Message}");
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            return PunterErrors.Exception("CreatePunterHandler.Commit()", $"{ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }

        #endregion
    }
}
