using AutenticazioneSvc.Shared.DTO;
using FluentValidation;

namespace AutenticazioneSvc.BusinessLayer.Validator;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .WithName("AccessToken")
            .WithMessage("AccessToken is required")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithName("RefreshToken")
            .WithMessage("RefreshToken is required")
            .WithSeverity(Severity.Warning);
    }
}
