using BLL.Contracts.Requests;
using FluentValidation;

namespace BLL.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Username).NotEmpty().Matches(@"^\S+$").WithMessage("Username cannot contain spaces");
        }
    }
}