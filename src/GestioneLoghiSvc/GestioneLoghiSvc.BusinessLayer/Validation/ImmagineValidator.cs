using FluentValidation;
using GestioneLoghiSvc.Shared.DTO;

namespace GestioneLoghiSvc.BusinessLayer.Validation;

public class CreateImmagineValidator : AbstractValidator<CreateImmagineDto>
{
    public CreateImmagineValidator()
    {
        RuleFor(x => x.FestaId)
            .NotEmpty()
            .WithMessage("'Festa Id' must not be empty.");

        RuleFor(x => x.Path)
            .NotEmpty()
            .WithMessage("'Path' must not be empty.");

        RuleFor(x => x.NomeImmagine)
            .NotEmpty()
            .WithMessage("'Nome Immagine' must not be empty.");

        RuleFor(x => x.Descrizione)
            .NotEmpty()
            .WithMessage("'Descrizione' must not be empty.");
    }
}