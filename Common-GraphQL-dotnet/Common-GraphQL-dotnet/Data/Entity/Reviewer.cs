﻿namespace Common_GraphQL_dotnet.Data.Entity
{
    public class Reviewer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public ICollection<GameReview> GameReviews { get; set; } = new HashSet<GameReview>();
    }
}
