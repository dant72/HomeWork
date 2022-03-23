using FluentValidation;

namespace HttpModels;

public class AccountValidator : AbstractValidator<Account> {
    public AccountValidator() {
        RuleFor(x => x.Login).NotEmpty().WithMessage("Enter login");
        RuleFor(x => x.HashPassword).NotEmpty().WithMessage("Enter password");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Enter e-mail");
    }

    private bool BeAValidPostcode(string postcode) {
        // custom postcode validating logic goes here

        return true;
    }
}