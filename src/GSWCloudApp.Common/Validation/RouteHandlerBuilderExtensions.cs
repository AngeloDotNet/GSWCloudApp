﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GSWCloudApp.Common.Validation;

/// <summary>
/// Extension methods for <see cref="RouteHandlerBuilder"/> to add validation.
/// </summary>
public static class RouteHandlerBuilderExtensions
{
    /// <summary>
    /// Adds validation to the route handler using the specified validator type.
    /// </summary>
    /// <typeparam name="T">The type of the validator.</typeparam>
    /// <param name="builder">The route handler builder.</param>
    /// <returns>The route handler builder with validation added.</returns>
    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class
    {
        return builder.AddEndpointFilter<ValidatorFilter<T>>().ProducesValidationProblem();
    }
}
