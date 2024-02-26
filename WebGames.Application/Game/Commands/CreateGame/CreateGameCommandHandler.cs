using AutoMapper;
using MediatR;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand>
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public CreateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        public async Task Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var newGame = mapper.Map<Domain.Entities.Game>(request);

            newGame.EncodeName();
            await gameRepository.Create(newGame);
        }
    }
}
