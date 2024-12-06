using System.Collections.Concurrent;
using System.Linq.Expressions;
using ErrorOr;
using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Sample.Aspire.ServiceDefaults.OptionalExtensions.FastEndpoints.Processors;

public sealed class ErrorOrPostProcessor : IGlobalPostProcessor
{
    public Task PostProcessAsync(IPostProcessorContext context, CancellationToken ct)
    {
        if (context.HttpContext.ResponseStarted() || context.Response is not IErrorOr errorOr)
            return Task.CompletedTask;

        if (!errorOr.IsError)
            return context.HttpContext.Response.SendAsync(GetValueFromErrorOr(errorOr), cancellation: ct);

        if (errorOr.Errors?.All(e => e.Type == ErrorType.Validation) is true)
        {
            return context.HttpContext.Response.SendErrorsAsync(
                failures:
                [
                    ..errorOr.Errors.Select(e => new ValidationFailure(
                        e.Code,
                        e.Description))
                ],
                cancellation: ct);
        }

        var problem = errorOr.Errors?.FirstOrDefault(e => e.Type != ErrorType.Validation);
        return problem?.Type switch
        {
            ErrorType.Conflict => context.HttpContext.Response.SendAsync("Invalid operation", StatusCodes.Status409Conflict, cancellation: ct),
            ErrorType.NotFound => context.HttpContext.Response.SendNotFoundAsync(ct),
            ErrorType.Unauthorized => context.HttpContext.Response.SendUnauthorizedAsync(ct),
            ErrorType.Forbidden => context.HttpContext.Response.SendForbiddenAsync(ct),
            null => throw new InvalidOperationException(),
            _ => Task.CompletedTask
        };
    }

    private static readonly ConcurrentDictionary<Type, Func<object, object>> ValueAccessors = new();

    private static object GetValueFromErrorOr(object errorOr)
    {
        ArgumentNullException.ThrowIfNull(errorOr);

        var tErrorOr = errorOr.GetType();
        if (!tErrorOr.IsGenericType || tErrorOr.GetGenericTypeDefinition() != typeof(ErrorOr<>))
            throw new InvalidOperationException("The provided object is not an instance of ErrorOr<>.");

        return ValueAccessors.GetOrAdd(tErrorOr, CreateValueAccessor)(errorOr);
    }

    private static Func<object, object> CreateValueAccessor(Type errorOrType)
    {
        var parameter = Expression.Parameter(typeof(object), "errorOr");
        var castToErrorOrType = Expression.Convert(parameter, errorOrType);
        var propertyAccess = Expression.Property(castToErrorOrType, "Value");
        var castToObject = Expression.Convert(propertyAccess, typeof(object));

        return Expression.Lambda<Func<object, object>>(castToObject, parameter).Compile();
    }
}