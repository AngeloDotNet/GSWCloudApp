using FluentValidation;
using GestioneDocumentiSvc.Shared.DTO;

namespace GestioneDocumentiSvc.BusinessLayer.Validation;

public class CreateDocumentoValidator : AbstractValidator<CreateDocumentoDto>
{
    public CreateDocumentoValidator()
    {
        RuleFor(x => x.FestaId)
            .NotEmpty()
            .WithMessage("'Festa Id' must not be empty.");

        RuleFor(x => x.Path)
            .NotEmpty()
            .WithMessage("'Path' must not be empty.");

        RuleFor(x => x.NomeDocumento)
            .NotEmpty()
            .WithMessage("'Nome Documento' must not be empty.");

        RuleFor(x => x.Descrizione)
            .NotEmpty()
            .WithMessage("'Descrizione' must not be empty.");
    }
}