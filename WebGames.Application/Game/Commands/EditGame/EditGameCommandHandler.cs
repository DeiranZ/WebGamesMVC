using MediatR;
using Microsoft.AspNetCore.Hosting;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.EditGame
{
    public class EditGameCommandHandler : IRequestHandler<EditGameCommand>
    {
        private readonly IGameRepository gameRepository;
		private readonly IUserContext userContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditGameCommandHandler(IGameRepository gameRepository, IUserContext userContext, IWebHostEnvironment webHostEnvironment)
        {
            this.gameRepository = gameRepository;
			this.userContext = userContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task Handle(EditGameCommand request, CancellationToken cancellationToken)
        {
			var currentUser = userContext.GetCurrentUser();

			if (currentUser == null || !(currentUser.IsInRole("Admin") || currentUser.IsInRole("Moderator")))
			{
				return;
			}

			var game = await gameRepository.GetByEncodedName(request.EncodedName);

            game!.Description = request.Description;
            game.Source = request.Source;
            game.ImageSource = request.ImageSource;

            if (request.Image != null)
            {
                var safeImageName = Path.GetRandomFileName();

                string ext = string.Empty;
                if (request.Image.ContentType == "image/jpeg") ext = ".jpeg";
                else if (request.Image.ContentType == "image/jpg") ext = ".jpg";
                else if (request.Image.ContentType == "image/png") ext = ".png";

                var finalImageName = Path.GetFileNameWithoutExtension(safeImageName) + ext;

                var imagesPath = Path.Combine(webHostEnvironment.WebRootPath, "gameImages");

                // Remove old file
                if (game.ImageName != null)
                {
                    var oldFilePath = Path.Combine(imagesPath, game.ImageName);
                    try
                    {
                        File.Delete(oldFilePath);
                    }
                    catch (Exception ex) 
                    {
                        
                    }
                }

                game.ImageName = finalImageName;

                var filePath = Path.Combine(imagesPath, finalImageName);
                await request.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            }

            await gameRepository.Commit();
        }
    }
}
