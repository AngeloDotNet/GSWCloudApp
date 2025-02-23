using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using FluentValidation;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Validator;

public class CreateSettingSenderValidator : AbstractValidator<CreateSettingSenderDto>
{
    public CreateSettingSenderValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithName("Sender name")
            .WithMessage("Sender name is required")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithName("Sender email")
            .WithMessage("Sender email is required")
            .EmailAddress()
            .WithMessage("Sender email is not valid")
            .WithSeverity(Severity.Warning);
    }
}