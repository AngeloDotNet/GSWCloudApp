using FluentValidation;
using InvioEmailSvc.Shared.DTO;

namespace InvioEmailSvc.BusinessLayer.Validator;

public class CreateEmailMessageValidator : AbstractValidator<CreateEmailMessageDto>
{
    public CreateEmailMessageValidator()
    {
        RuleForEach(m => m.To)
            .NotEmpty()
            .EmailAddress();

        //RuleForEach(m => m.Cc)
        //    .NotEmpty()
        //    .EmailAddress();

        //RuleForEach(m => m.Bcc)
        //    .NotEmpty()
        //    .EmailAddress();

        //RuleForEach(m => m.ReplyTo)
        //    .NotEmpty()
        //    .EmailAddress();

        RuleFor(m => m)
            .Must(m => m.To.Any() || m.Cc.Any() || m.Bcc.Any())
            .WithName("Recipients")
            .WithMessage("At least one recipient is required")
            .WithSeverity(Severity.Warning);

        RuleFor(m => m.Subject)
            .NotEmpty()
            .WithName("Email Subject")
            .WithMessage("The email subject must not be empty.")
            .WithSeverity(Severity.Warning);

        RuleFor(m => m.TextContent)
            .NotEmpty()
            .Must(m => !string.IsNullOrWhiteSpace(m))
            .WithName("Email Content")
            .WithMessage("Email content must not be empty.")
            .WithSeverity(Severity.Warning);
    }
}
