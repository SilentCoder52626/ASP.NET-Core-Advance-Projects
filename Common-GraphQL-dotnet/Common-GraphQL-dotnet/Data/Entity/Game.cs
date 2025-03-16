namespace Common_GraphQL_dotnet.Data.Entity
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public DateTime ReleasedOn { get; set; }
        public ICollection<GameReview> Reviews { get; set; } = new HashSet<GameReview>();
    }
}
