using MediatR;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.GameGenre.Commands.AddGenreToGame
{
    public class AddGenreToGameCommandHandler : IRequestHandler<AddGenreToGameCommand>
    {
        private readonly IGameRepository gameRepository;
        private readonly IGenreRepository genreRepository;

        public AddGenreToGameCommandHandler(IGameRepository gameRepository, IGenreRepository genreRepository)
        {
            this.gameRepository = gameRepository;
            this.genreRepository = genreRepository;
        }

        public async Task Handle(AddGenreToGameCommand request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetByEncodedName(request.GameEncodedname);
            var genre = await genreRepository.GetByEncodedName(request.GenreEncodedname);

            if (game == null || genre == null) return;
            if (game.Genres.Contains(genre)) return;

            game.Genres.Add(genre);

            await gameRepository.Commit();
        }
    }
}
