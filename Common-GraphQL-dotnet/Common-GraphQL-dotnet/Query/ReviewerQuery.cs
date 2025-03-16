using Common_GraphQL_dotnet.Data.Context;
using Common_GraphQL_dotnet.DTO;
using Microsoft.EntityFrameworkCore;

namespace Common_GraphQL_dotnet.Query
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [GraphQLDescription("Queries to manage and query data")]
    public class ReviewerQuery
    {
        [GraphQLDescription("Get a list of reviewers")]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ReviewerDto> GetReviewers([Service] AppDbContext context) => context
           .Reviewers
           .AsNoTracking()
           .TagWith($"{nameof(ReviewerQuery)}::{nameof(GetReviewers)}")
           .Select(reviewer => new ReviewerDto
           {
               Email = reviewer.Email,
               ReviewerId = reviewer.Id.ToString(),
               Name = reviewer.Name,
               PictureUrl = reviewer.Picture,
               Username = reviewer.Username
           });
    }
}
