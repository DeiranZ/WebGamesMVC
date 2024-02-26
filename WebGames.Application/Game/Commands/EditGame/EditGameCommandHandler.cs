using AutoMapper;
using MediatR;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.EditGame
{
    public class EditGameCommandHandler : IRequestHandler<EditGameCommand>
    {
        private readonly IGameRepository gameRepository;

        public EditGameCommandHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task Handle(EditGameCommand request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetByEncodedName(request.EncodedName);

            game!.Description = request.Description;
            game.Source = request.Source;

            await gameRepository.Commit();
        }
    }
}
