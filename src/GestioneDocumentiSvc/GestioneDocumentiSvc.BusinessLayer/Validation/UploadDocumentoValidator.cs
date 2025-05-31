using FluentValidation;
using GestioneDocumentiSvc.Shared.DTO;

namespace GestioneDocumentiSvc.BusinessLayer.Validation;

public class UploadDocumentoValidator : AbstractValidator<UploadDocumentoDto>
{
    public UploadDocumentoValidator()
    {
        RuleFor(x => x.Documento)
            .NotNull()
            .WithName("Documento")
            .WithMessage("Documento is required")
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
