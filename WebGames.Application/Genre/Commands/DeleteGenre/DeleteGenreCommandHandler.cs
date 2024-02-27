using MediatR;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Genre.Commands.DeleteGenre
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
    {
        private readonly IGenreRepository genreRepository;
        private readonly IUserContext userContext;

        public DeleteGenreCommandHandler(IGenreRepository genreRepository, IUserContext userContext)
        {
            this.genreRepository = genreRepository;
            this.userContext = userContext;
        }

        public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            if (currentUser == null || !(currentUser.IsInRole("Admin") || currentUser.IsInRole("Moderator")))
            {
                return;
            }

            var genre = genreRepository.GetByEncodedName(request.EncodedName);

            await genreRepository.Delete(genre.Result);
        }
    }
}
