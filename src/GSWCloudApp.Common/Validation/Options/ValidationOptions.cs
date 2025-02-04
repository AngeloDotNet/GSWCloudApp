using Microsoft.AspNetCore.Http;

namespace GSWCloudApp.Common.Validation.Options;

/// <summary>
/// Options for configuring validation behavior.
/// </summary>
public class ValidationOptions
{
    /// <summary>
    /// Gets or sets the format of the error response.
    /// </summary>
    public ErrorResponseFormat ErrorResponseFormat { get; set; }

    /// <summary>
    /// Gets or sets the factory function to create a validation error title message.
    /// </summary>
    /// <remarks>
    /// The function takes an <see cref="EndpointFilterInvocationContext"/> and a dictionary of validation errors,
    /// and returns a string representing the validation error title message.
    /// </remarks>
    public Func<EndpointFilterInvocationContext, IDictionary<string, string[]>, string>? ValidationErrorTitleMessageFactory { get; set; }
}
