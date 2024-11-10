using ConfigurazioniSvc.Shared.DTO;
using FluentValidation;

namespace ConfigurazioniSvc.BusinessLayer.Validation;

public class CreateConfigurazioneValidator : AbstractValidator<CreateConfigurazioneDto>
{
    public CreateConfigurazioneValidator()
    {
        RuleFor(x => x.FestaId)
            .NotEmpty();

        RuleFor(x => x.Chiave)
            .NotEmpty();

        RuleFor(x => x.Valore)
            .NotEmpty();

        RuleFor(x => x.Tipo)
            .NotEmpty();

        RuleFor(x => x.Scope)
            .IsInEnum();
    }
}

public class EditConfigurazioneValidator : AbstractValidator<EditConfigurazioneDto>
{ }