using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.Validation;

public static class DependencyInjection
{
    /// <summary>
    /// Configures FluentValidation with the specified validator.
    /// </summary>
    /// <typeparam name="TValidator">The type of the FluentValidation validator.</typeparam>
    /// <param name="services">The service collection to add the FluentValidation configuration to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureFluentValidation<TValidator>(this IServiceCollection services) where TValidator : IValidator
        => services.AddValidatorsFromAssemblyContaining<TValidator>();
}
