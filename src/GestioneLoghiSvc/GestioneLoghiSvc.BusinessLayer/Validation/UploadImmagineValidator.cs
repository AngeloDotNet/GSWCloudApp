using FluentValidation;
using GestioneLoghiSvc.Shared.DTO;

namespace GestioneLoghiSvc.BusinessLayer.Validation;

public class UploadImmagineValidator : AbstractValidator<UploadImmagineDto>
{
    public UploadImmagineValidator()
    {
        RuleFor(x => x.Immagine)
            .NotNull()
            .WithName("Immagine")
            .WithMessage("Immagine is required")
            .WithSeverity(Severity.Warning);

        //RuleFor(x => x.FestaId)
        //    .NotEmpty()
        //    .WithName("FestaId")
        //    .WithMessage("FestaId is required")
        //    .WithSeverity(Severity.Warning);

        RuleFor(x => x.NomeDocumento)
            .NotEmpty()
            .WithName("NomeDocumento")
            .WithMessage("NomeDocumento is required")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Descrizione)
            .NotEmpty()
            .WithName("Descrizione")
            .WithMessage("Descrizione is required")
            .WithSeverity(Severity.Warning);
    }
}
