﻿
using Common_GraphQL_dotnet.Exceptions;
using FluentValidation;

namespace Common_GraphQL_dotnet.Error
{
    public class ServerErrorFilter : IErrorFilter
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;

        public ServerErrorFilter(ILogger logger, IWebHostEnvironment environment)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        public IError OnError(IError error)
        {
            if (error.Exception is ValidationException validationException)
            {
                _logger.LogError(validationException, "There is a validation error");

                var errorBuilder = ErrorBuilder
                    .New()
                    .SetMessage("There is a validation error")
                    .SetCode(ServerErrorCode.BadUserInput)
                    .SetPath(error.Path);

                foreach (var validationFailure in validationException.Errors)
                {
                    errorBuilder.SetExtension(
                        $"{ServerErrorCode.BadUserInput}_{validationFailure.PropertyName.ToUpper()}",
                        validationFailure.ErrorMessage);
                }

                return errorBuilder.Build();
            }
            else if (error.Exception is GameNotFoundException gameNotFoundException)
            {
                _logger.LogError(gameNotFoundException, "Game not found");

                return ErrorBuilder
                    .New()
                    .SetMessage($"A game having id '{gameNotFoundException.GameId} could not found")
                    .SetCode(ServerErrorCode.ResourceNotFound)
                    .SetPath(error.Path)
                    .SetExtension($"{ServerErrorCode.ResourceNotFound}_GAME_ID", gameNotFoundException.GameId)
                    .Build();
            }
            else if (error.Exception is GameReviewNotFoundException gameReviewNotFoundException)
            {
                _logger.LogError(gameReviewNotFoundException, "Game review not found");

                return ErrorBuilder
                    .New()
                    .SetMessage($"A game review having game id '{gameReviewNotFoundException.GameId}' and reviewer id '{gameReviewNotFoundException.ReviewerId}' could not found")
                    .SetCode(ServerErrorCode.ResourceNotFound)
                    .SetPath(error.Path)
                    .SetExtension($"{ServerErrorCode.ResourceNotFound}_GAME_ID", gameReviewNotFoundException.GameId)
                    .SetExtension($"{ServerErrorCode.ResourceNotFound}_REVIEWER_ID", gameReviewNotFoundException.ReviewerId)
                    .Build();
            }
            else if (error.Exception is ReviewerNotFoundException reviewerNotFoundException)
            {
                _logger.LogError(reviewerNotFoundException, "Game not found");

                return ErrorBuilder
                    .New()
                    .SetMessage($"A reviewer having id '{reviewerNotFoundException.ReviewerId} could not found")
                    .SetCode(ServerErrorCode.ResourceNotFound)
                    .SetPath(error.Path)
                    .SetExtension($"{ServerErrorCode.ResourceNotFound}_REVIEWER_ID", reviewerNotFoundException.ReviewerId)
                    .Build();
            }

            _logger.LogError(error.Exception, error.Message);

            if (_environment.IsDevelopment())
                return error;

            return ErrorBuilder
               .New()
               .SetMessage("An unexpected server fault occurred")
               .SetCode(ServerErrorCode.ServerFault)
               .SetPath(error.Path)
               .Build();
        }
    }
}
