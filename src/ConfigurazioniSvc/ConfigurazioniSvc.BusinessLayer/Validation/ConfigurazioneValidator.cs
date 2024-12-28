using ConfigurazioniSvc.Shared.DTO;
using FluentValidation;

namespace ConfigurazioniSvc.BusinessLayer.Validation;

public class CreateConfigurazioneValidator : AbstractValidator<CreateConfigurazioneDto>
{
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

public class EditConfigurazioneValidator : AbstractValidator<EditConfigurazioneDto>
{
    public EditConfigurazioneValidator()
    { }
}
