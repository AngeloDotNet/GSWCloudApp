using ConfigurazioniSvc.Shared.DTO;
using FluentValidation;

namespace ConfigurazioniSvc.BusinessLayer.Validation;

/// <summary>
/// Validator for creating configuration settings.
/// </summary>
public class CreateConfigurazioneValidator : AbstractValidator<CreateConfigurazioneDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateConfigurazioneValidator"/> class.
    /// </summary>
    public CreateConfigurazioneValidator()
    {
        RuleFor(x => x.FestaId)
            .NotEmpty()
            .WithMessage("'Festa Id' must not be empty.");

        RuleFor(x => x.Chiave)
            .NotEmpty()
            .WithMessage("'Chiave' must not be empty.");

        RuleFor(x => x.Valore)
            .NotEmpty()
            .WithMessage("'Valore' must not be empty.");

        RuleFor(x => x.Tipo)
            .NotEmpty()
            .WithMessage("'Tipo' must not be empty.");

        RuleFor(x => x.Scope)
            .IsInEnum()
            .WithMessage("'Scope' must be a valid enum value.");
    }
}

/// <summary>
/// Validator for editing configuration settings.
/// </summary>
public class EditConfigurazioneValidator : AbstractValidator<EditConfigurazioneDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EditConfigurazioneValidator"/> class.
    /// </summary>
    public EditConfigurazioneValidator()
    {
        // Add validation rules here if needed
    }
}
