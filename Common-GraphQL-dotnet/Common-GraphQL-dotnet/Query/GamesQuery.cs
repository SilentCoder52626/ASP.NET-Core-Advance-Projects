using Common_GraphQL_dotnet.DTO;
using Common_GraphQL_dotnet.Model;

namespace Common_GraphQL_dotnet.Query
{
    [GraphQLDescription("Query games")]
    public sealed class GamesQuery
    {
        [GraphQLDescription("Get list of games")]
        public IEnumerable<GameDto> GetGames() => GameData.Games;

        [GraphQLDescription("Find game by id")]
        public GameDto? FindGameById(Guid gameId) =>
           GameData.Games.FirstOrDefault(game => game.GameId == gameId);
    }
}
