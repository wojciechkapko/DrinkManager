using BLL.Contracts.Requests;
using FluentValidation;

namespace BLL.Validators
{
    public class ReviewValidator : AbstractValidator<ReviewCreateRequest>
    {
        public ReviewValidator()
        {
            RuleFor(x => x.ReviewText).NotEmpty();
            RuleFor(x => x.ReviewScore).NotEmpty().Matches(@"^[1-5]*$").WithMessage("Your score is invalid");
            RuleFor(x => x.AuthorName).NotEmpty();
        }
    }
}
