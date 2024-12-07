using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GSWCloudApp.Common.Validation;

/// <summary>  
/// A filter that validates the input of an endpoint using FluentValidation.  
/// </summary>  
/// <typeparam name="T">The type of the input to validate.</typeparam>  
internal class ValidatorFilter<T>(IValidator<T> validator, IOptions<ValidationOptions> options) : IEndpointFilter where T : class
{
    private readonly ValidationOptions validationOptions = options.Value;

    /// <summary>  
    /// Invokes the filter to validate the input and proceed to the next filter or endpoint if valid.  
    /// </summary>  
    /// <param name="context">The context of the endpoint invocation.</param>  
    /// <param name="next">The delegate to invoke the next filter or endpoint.</param>  
    /// <returns>The result of the next filter or endpoint if validation is successful; otherwise, a validation problem result.</returns>  
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.Arguments.FirstOrDefault(a => a?.GetType() == typeof(T)) is not T input)
        {
            return TypedResults.UnprocessableEntity();
        }

        var validationResult = await validator.ValidateAsync(input);

        if (validationResult.IsValid)
        {
            return await next(context);
        }

        var errors = validationResult.ToDictionary();

        var result = TypedResults.Problem(
            statusCode: StatusCodes.Status422UnprocessableEntity,
            instance: context.HttpContext.Request.Path,
            title: validationOptions.ValidationErrorTitleMessageFactory?.Invoke(context, errors) ?? "One or more validation errors occurred",
            extensions: new Dictionary<string, object?>(StringComparer.Ordinal)
            {
                ["traceId"] = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
                ["errors"] = validationOptions.ErrorResponseFormat == ErrorResponseFormat.Default
                    ? errors : errors.SelectMany(e
                        => e.Value.Select(m => new { Name = e.Key, Message = m })).ToArray()
            }
        );

        return result;
    }
}
