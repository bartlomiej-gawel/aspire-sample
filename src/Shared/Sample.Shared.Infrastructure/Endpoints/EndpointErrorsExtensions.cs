using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Sample.Shared.Infrastructure.Endpoints;

public static class EndpointErrorsExtensions
{
    public static IResult ToProblemResult(this List<Error> errors)
    {
        if (errors.Count is 0)
            return TypedResults.Problem();

        if (errors.All(error => error.Type == ErrorType.Validation))
            return errors.ToValidationProblemResult();

        return errors[0].ToProblemResult();
    }

    private static ProblemHttpResult ToProblemResult(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError,
        };

        return TypedResults.Problem(
            statusCode: statusCode,
            title: error.Description);
    }

    private static ValidationProblem ToValidationProblemResult(this IEnumerable<Error> errors)
    {
        var validationErrors = errors
            .GroupBy(error => error.Code)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(error => error.Description)
                    .ToArray());

        return TypedResults.ValidationProblem(validationErrors);
    }
}