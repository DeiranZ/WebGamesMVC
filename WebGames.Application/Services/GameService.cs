using AutoMapper;
using WebGames.Application.Game;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        public async Task Create(GameDto game)
        {
            var newGame = mapper.Map<Domain.Entities.Game>(game);

            newGame.EncodeName();
            await gameRepository.Create(newGame);
        }

		public async Task<IEnumerable<GameDto>> GetAll()
		{
            var games = await gameRepository.GetAll();
            var gameDtos = mapper.Map<IEnumerable<GameDto>>(games);

            return gameDtos;
		}
	}
}
