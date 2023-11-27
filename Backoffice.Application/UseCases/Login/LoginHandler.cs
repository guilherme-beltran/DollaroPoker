using Backoffice.Application.Interfaces.Users;
using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Interfaces.Services;
using Backoffice.Domain.Shared;
using System.Net;

namespace Backoffice.Application.UseCases.Login;

internal sealed class LoginHandler : ILoginHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async  Task<Response> Handle(LoginCommand request)
    {
        #region Validate request

        try
        {
            request.Validate();
            if (!request.IsValid)
                return UserErrors.SendNotifications(notifications: request.Notifications);
        }
        catch (NullReferenceException)
        {
            return UserErrors.ReturnNullReference("CreateUserHandler");
        }
        catch (Exception)
        {
            return UserErrors.ExceptionResult;
        }

        #endregion

        #region Get User
        User user;
        try
        {
            user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user is null)
                return UserErrors.NotFound("LoginHandler.user", search: $"{request.Username}");
        }
        catch (Exception)
        {
            return UserErrors.ExceptionResult;
        }

        #endregion

        #region Verify password

        var isEqualsPassword = user!.VerifyPassword(request.Password);
        if (!isEqualsPassword)
            return UserErrors.InvalidPassword;

        #endregion

        #region Generate Token and response for authenticate user

        LoginResponse response;

        try
        {
            response = new LoginResponse(statusCode: HttpStatusCode.OK,
                                         id: user.UserId,
                                         username: user.Username!,
                                         message: $"{user.Name} was successfully authenticated!");

            response.Token = _tokenService.GenerateToken(response);

            return Response.SucessLogin(response);

        }
        catch (Exception)
        {
            return UserErrors.ExceptionResult;
        }

        #endregion
    }
}
