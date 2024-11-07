using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GSWCloudApp.Common.Validation;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class
    {
        builder.AddEndpointFilter<ValidatorFilter<T>>()
           .ProducesValidationProblem();

        return builder;
    }
}