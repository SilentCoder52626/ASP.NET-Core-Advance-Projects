using FluentValidation;

namespace Common_Fluent_Validation_dotnet.Models.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationValidator()
        {
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotEmpty().MinimumLength(4).Must(IsValidName);
            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Email).EmailAddress().WithName("Email address").WithMessage("{PropertyName} is invalid! Please check!");
            RuleFor(x => x.Password).Equal(z => z.ConfirmPassword).WithMessage("Passwords do not match!");
        }
        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}
