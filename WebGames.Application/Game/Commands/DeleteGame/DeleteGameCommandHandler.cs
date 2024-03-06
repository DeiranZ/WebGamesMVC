using MediatR;
using Microsoft.AspNetCore.Hosting;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
    {
        private readonly IGameRepository gameRepository;
		private readonly IUserContext userContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DeleteGameCommandHandler(IGameRepository gameRepository, IUserContext userContext, IWebHostEnvironment webHostEnvironment)
        {
            this.gameRepository = gameRepository;
			this.userContext = userContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
			var currentUser = userContext.GetCurrentUser();

			if (currentUser == null || !(currentUser.IsInRole("Admin") || currentUser.IsInRole("Moderator")))
			{
				return;
			}

			var game = await gameRepository.GetByEncodedName(request.EncodedName);

            if (game.ImageName != null)
            {
                var imagesPath = Path.Combine(webHostEnvironment.WebRootPath, "gameImages");

                var oldFilePath = Path.Combine(imagesPath, game.ImageName);
                try
                {
                    File.Delete(oldFilePath);
                }
                catch (Exception ex)
                {

                }
            }

            if (game.SourceName != null)
            {
                var gameFilesPath = Path.Combine(webHostEnvironment.WebRootPath, "gameFiles");

                var oldDirectory = Path.Combine(gameFilesPath, game.SourceName);
                try
                {
                    Directory.Delete(oldDirectory, true);
                }
                catch (Exception ex)
                {

                }
            }

            await gameRepository.Delete(game);
        }
    }
}
