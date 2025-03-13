using Common_GraphQL_dotnet.DTO;
using Common_GraphQL_dotnet.Exceptions;
using Common_GraphQL_dotnet.Model;
using FluentValidation;

namespace Common_GraphQL_dotnet.Mutations
{
    [GraphQLDescription("Manage games")]
    public sealed class GamesMutation
    {
        private readonly AbstractValidator<GameReviewDto> _validator;

        public GamesMutation(AbstractValidator<GameReviewDto> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [GraphQLDescription("Submit a game review")]
        public GameReviewDto SubmitGameReview(GameReviewDto gameReview)
        {
            _validator.ValidateAndThrow(gameReview);

            var game = GameData
               .Games
               .FirstOrDefault(game => game.GameId == gameReview.GameId)
               ?? throw GameNotFoundException.WithGameId(gameReview.GameId);

            var gameReviewFromDb = game.Reviews.FirstOrDefault(review =>
               review.GameId == gameReview.GameId && review.ReviewerId == gameReview.ReviewerId);

            if (gameReviewFromDb is null)
            {
                game.Reviews.Add(gameReview);
            }
            else
            {
                gameReviewFromDb.Rating = gameReview.Rating;
                gameReviewFromDb.Summary = gameReview.Summary;
            }

            return gameReview;
        }

        [GraphQLDescription("Remove a game review")]
        public GameReviewDto DeleteGameReview(Guid gameId, Guid reviewerId)
        {
            var game = GameData
               .Games
               .FirstOrDefault(game => game.GameId == gameId)
               ?? throw GameNotFoundException.WithGameId(gameId);

            var gameReviewFromDb = game
               .Reviews
               .FirstOrDefault(review => review.GameId == gameId && review.ReviewerId == reviewerId)
               ?? throw GameReviewNotFoundException.WithGameReviewId(gameId,reviewerId);

            game.Reviews.Remove(gameReviewFromDb);

            return gameReviewFromDb;
        }
    }

}
