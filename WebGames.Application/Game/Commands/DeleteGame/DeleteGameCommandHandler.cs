using MediatR;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
    {
        private readonly IGameRepository gameRepository;
		private readonly IUserContext userContext;

		public DeleteGameCommandHandler(IGameRepository gameRepository, IUserContext userContext)
        {
            this.gameRepository = gameRepository;
			this.userContext = userContext;
		}

        public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
			var currentUser = userContext.GetCurrentUser();

			if (currentUser == null || !(currentUser.IsInRole("Admin") || currentUser.IsInRole("Moderator")))
			{
				return;
			}

			var game = gameRepository.GetByEncodedName(request.EncodedName);

            await gameRepository.Delete(game.Result);
        }
    }
}
