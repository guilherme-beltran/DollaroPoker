namespace Backoffice.Domain.Shared;

public class Response
{
    private Response(bool isSuccess, Error error, object? obj = null)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Obj = obj!;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public object Obj { get; }

    public static Response Sucess() => new(true, Error.None);
    public static Response SucessLogin(object obj) => new(true, Error.None, obj);
    public static Response Failure(Error error) => new(false, error);
    public static Response NotFound(Error error) => new(false, error);

}