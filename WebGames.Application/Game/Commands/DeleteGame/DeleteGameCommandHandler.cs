using AutoMapper;
using MediatR;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
    {
        private readonly IGameRepository gameRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = gameRepository.GetByEncodedName(request.EncodedName);

            await gameRepository.Delete(game.Result);
        }
    }
}
