using Backoffice.Application.Interfaces;
using Backoffice.Application.Interfaces.Users;
using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Interfaces.UnitOfWork;
using Backoffice.Domain.Shared;
using MySql.Data.MySqlClient;

namespace Backoffice.Application.UseCases.Users.Create;

internal sealed class CreateUserHandler : ICreateUserHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IJurisdictionRepository _jurisdictionRepository;
    private readonly ISequenceRepository _sequenceRepository;

    public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IJurisdictionRepository jurisdictionRepository, ISequenceRepository sequenceRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _jurisdictionRepository = jurisdictionRepository;
        _sequenceRepository = sequenceRepository;
    }

    public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
            return PunterErrors.ReturnNullReference("CreateUserHandler");
        }
        catch (Exception)
        {
            return PunterErrors.ExceptionResult;
        }

        #endregion

        #region Checks if User already registered

        try
        {
            var isRegistered = await _userRepository.IsRegisteredAsync(request.Username!);
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
                return PunterErrors.NotFound("CreateUserHandler.jurisdiction", search: $"Id {request.Club}");
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
            sequence = await _sequenceRepository.GetSequenceUserAsync();
            if (sequence is null)
                return PunterErrors.NotFound("CreateUserHandler.sequence", search: $"Sequence Id User");
        }
        catch (Exception)
        {
            return PunterErrors.ExceptionResult;
        }

        #endregion

        #region Create punter

        sequence!.UpdateSequence();

        var user = User.Create(id: sequence.NextSequence!,
                               username: request.Username!,
                               password: request.Password,
                               jurisdiction: jurisdiction);

        user.Encrypt();

        try
        {
            _unitOfWork.BeginTransaction();

            await _userRepository.Insert(user: user, cancellationToken: cancellationToken);

            await _unitOfWork.Commit(cancellationToken: cancellationToken);

            return Response.Sucess($"{request.Username} created successfully.");
        }
        catch (MySqlException ex)
        {
            _unitOfWork.Rollback();
            return PunterErrors.Exception("CreateUserHandler.Commit()", $"{ex.Message}");
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            return PunterErrors.Exception("CreateUserHandler.Commit()", $"{ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }

        #endregion
    }
}
