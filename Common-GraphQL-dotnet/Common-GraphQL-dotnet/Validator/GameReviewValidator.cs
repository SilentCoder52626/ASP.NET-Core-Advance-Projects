using Common_GraphQL_dotnet.DTO;
using Common_GraphQL_dotnet.Model;
using FluentValidation;

namespace Common_GraphQL_dotnet.Validator
{
    public sealed class GameReviewValidator : AbstractValidator<GameReviewDto>
    {
        public GameReviewValidator()
        {
            RuleFor(e => e.GameId)
                .NotNull()
                .NotEmpty();

            RuleFor(e => e.Rating)
              .LessThanOrEqualTo(100)
              .GreaterThanOrEqualTo(0);

            RuleFor(e => e.Summary)
              .NotNull()
              .NotEmpty()
              .MinimumLength(20)
              .MaximumLength(500);
        }
    }
}
