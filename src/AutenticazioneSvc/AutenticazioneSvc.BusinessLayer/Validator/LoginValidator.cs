using AutenticazioneSvc.Shared.DTO;
using FluentValidation;

namespace AutenticazioneSvc.BusinessLayer.Validator;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserName)
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
