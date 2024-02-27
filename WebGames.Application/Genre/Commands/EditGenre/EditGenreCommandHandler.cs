using MediatR;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Genre.Commands.EditGenre
{
    public class EditGenreCommandHandler : IRequestHandler<EditGenreCommand>
    {
        private readonly IGenreRepository genreRepository;
        private readonly IUserContext userContext;

        public EditGenreCommandHandler(IGenreRepository genreRepository, IUserContext userContext)
        {
            this.genreRepository = genreRepository;
            this.userContext = userContext;
        }

        public async Task Handle(EditGenreCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            if (currentUser == null || !(currentUser.IsInRole("Admin") || currentUser.IsInRole("Moderator")))
            {
                return;
            }

            var game = await genreRepository.GetByEncodedName(request.EncodedName);

            game!.Description = request.Description;

            await genreRepository.Commit();
        }
    }
}
