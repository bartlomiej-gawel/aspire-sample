using MediatR;
using Microsoft.Extensions.Logging;
using Sample.Shared.Infrastructure.Exceptions;

namespace Sample.Shared.Infrastructure.Mediator.Behaviors;

public sealed class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;

    public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception for {RequestName}", typeof(TRequest).Name);
            throw new CustomException(typeof(TRequest).Name, innerException: exception);
        }
    }
}