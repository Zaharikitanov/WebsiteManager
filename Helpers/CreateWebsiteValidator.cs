using FluentValidation;
using WebsiteManager.Models.View;

namespace WebsiteManager.Helpers
{
    public class AddNewWebsiteValidator : AbstractValidator<CreateNewWebsiteData>
    {
        public AddNewWebsiteValidator()
        {
            RuleFor(website => website.Name)
                .NotEmpty()
                .WithMessage("Required")
                .NotNull()
                .WithMessage("Required")
                .MaximumLength(150);

            RuleFor(website => website.URL)
                .NotEmpty()
                .WithMessage("Required")
                .NotNull()
                .WithMessage("Required")
                .Matches(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$")
                .WithMessage("Invalid website url format.");

            RuleFor(website => website.Category)
                .IsInEnum()
                .WithMessage("Required");

            RuleFor(website => website.HomepageSnapshot)
                .NotEmpty()
                .WithMessage("Required")
                .NotNull()
                .WithMessage("Required")
                .MaximumLength(150);

            RuleFor(website => website.LoginDetails.Email)
                .NotEmpty()
                .WithMessage("Required")
                .NotNull()
                .WithMessage("Required")
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(website => website.LoginDetails.Password)
                .NotEmpty()
                .WithMessage("Required")
                .NotNull()
                .WithMessage("Required")
                .MinimumLength(4)
                .WithMessage("Password must be at least 4 characters long.")
                .MaximumLength(30)
                .WithMessage("Password cannot be more than 30 characters long");
        }
    }
}
