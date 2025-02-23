using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using FluentValidation;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Validator;

public class CreateSettingSmtpValidator : AbstractValidator<CreateSettingSmtpDto>
{
    public CreateSettingSmtpValidator()
    {
        RuleFor(x => x.Host)
            .NotEmpty()
            .WithName("Host")
            .WithMessage("Host is required")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Port)
            .GreaterThan(0)
            .WithName("Port")
            .WithMessage("Port must be greater than 0")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Security)
            .IsInEnum()
            .WithName("Security")
            .WithMessage("Security is not valid")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithName("Username")
            .WithMessage("Username is required")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithName("Password")
            .WithMessage("Password is required")
            .WithSeverity(Severity.Warning);
    }
}