using AutoMapper;
using MediatR;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.EditGame
{
    public class EditGameCommandHandler : IRequestHandler<EditGameCommand>
    {
        private readonly IGameRepository gameRepository;
		private readonly IUserContext userContext;

		public EditGameCommandHandler(IGameRepository gameRepository, IUserContext userContext)
        {
            this.gameRepository = gameRepository;
			this.userContext = userContext;
		}

        public async Task Handle(EditGameCommand request, CancellationToken cancellationToken)
        {
			var currentUser = userContext.GetCurrentUser();

			if (currentUser == null || (currentUser.IsInRole("Admin") && currentUser.IsInRole("Moderator")))
			{
				return;
			}

			var game = await gameRepository.GetByEncodedName(request.EncodedName);

            game!.Description = request.Description;
            game.Source = request.Source;

            await gameRepository.Commit();
        }
    }
}
