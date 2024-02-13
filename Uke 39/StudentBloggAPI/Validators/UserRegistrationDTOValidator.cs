using FluentValidation;
using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Validators;

public class UserRegistrationDTOValidator : AbstractValidator<UserRegistrationDTO>
{
    // constructor
    public UserRegistrationDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username kan ikke være null")
            .MinimumLength(3).WithMessage("Username må være på minst 3 tegn")
            .MaximumLength(16).WithMessage("Username kan ikke være lengre enn 16 tegn");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("email må være med.")
            .EmailAddress().WithMessage("Må ha en gydlig e-mail");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Forname kan ikke være null");

        RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("Lastname kan ikke være null");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password må være med")
            .MinimumLength(8).WithMessage("Passord må være på minst 8 tegn")
            .MaximumLength(16).WithMessage("Passord kan ikke være lengre enn 16 tegn")
            .Matches(@"[0-9]+").WithMessage("Må ha minst 1 tall i passordet")
            .Matches(@"[A-Z]+").WithMessage("Passord må ha minst en stor bokstav")
            .Matches(@"[a-z]+").WithMessage("Passord må ha minst en liten bokstav")
            .Matches(@"[!?*#_@%&]+").WithMessage("Passord må ha minst en spesial tegn ('!, ?, *, #, _,@, %, & ')");
            
    }
}
