using AutoMapper;
using MediatR;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand>
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;
		private readonly IUserContext userContext;

		public CreateGameCommandHandler(IGameRepository gameRepository, IMapper mapper, IUserContext userContext)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
			this.userContext = userContext;
		}

        public async Task Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var newGame = mapper.Map<Domain.Entities.Game>(request);

            newGame.CreatedById = userContext.GetCurrentUser().Id;

            newGame.EncodeName();
            await gameRepository.Create(newGame);
        }
    }
}
