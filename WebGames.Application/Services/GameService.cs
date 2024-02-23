using WebGames.Domain.Entities;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }
        public async Task Create(Game game)
        {
            game.EncodeName();
            await gameRepository.Create(game);
        }
    }
}
