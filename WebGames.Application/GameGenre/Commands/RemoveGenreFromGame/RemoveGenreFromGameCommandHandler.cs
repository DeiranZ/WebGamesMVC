using MediatR;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.GameGenre.Commands.RemoveGenreFromGame
{
    public class RemoveGenreFromGameCommandHandler : IRequestHandler<RemoveGenreFromGameCommand>
    {
        private readonly IGameGenreRepository gameGenreRepository;

        public RemoveGenreFromGameCommandHandler(IGameGenreRepository gameGenreRepository)
        {
            this.gameGenreRepository = gameGenreRepository;
        }

        public async Task Handle(RemoveGenreFromGameCommand request, CancellationToken cancellationToken)
        {
            await gameGenreRepository.RemoveGenreFromGame(request.GameEncodedname, request.GenreEncodedname);
        }
    }
}
