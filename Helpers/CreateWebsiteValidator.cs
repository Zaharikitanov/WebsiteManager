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
                .NotNull()
                .MaximumLength(150);

            RuleFor(website => website.URL)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");

            RuleFor(website => website.Category)
                .NotEmpty()
                .NotNull()
                .MaximumLength(150);

            RuleFor(website => website.HomepageSnapshot)
                .NotEmpty()
                .NotNull()
                .MaximumLength(150);

            RuleFor(website => website.LoginDetails.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(website => website.LoginDetails.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(150);
        }
    }
}
