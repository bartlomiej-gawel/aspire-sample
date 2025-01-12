using ErrorOr;

namespace Sample.Shared.Infrastructure.Exceptions;

public sealed class CustomException : Exception
{
    public string RequestName { get; }
    public List<Error>? Errors { get; }

    public CustomException(
        string requestName,
        List<Error>? errors = null,
        Exception? innerException = null) : base("Application exception", innerException)
    {
        RequestName = requestName;
        Errors = errors;
    }
}