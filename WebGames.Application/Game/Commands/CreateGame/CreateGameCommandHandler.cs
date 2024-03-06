using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand>
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;
		private readonly IUserContext userContext;
		private readonly IWebHostEnvironment webHostEnvironment;

		public CreateGameCommandHandler(IGameRepository gameRepository, IMapper mapper, IUserContext userContext, IWebHostEnvironment webHostEnvironment)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
			this.userContext = userContext;
			this.webHostEnvironment = webHostEnvironment;
		}

        public async Task Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            if (currentUser == null || !(currentUser.IsInRole("Admin") || currentUser.IsInRole("Moderator")))
            {
                return;
            }

            var newGame = mapper.Map<Domain.Entities.Game>(request);

            newGame.CreatedById = currentUser.Id;

            newGame.EncodeName();

            if (request.Image != null)
            {
                var safeImageName = Path.GetRandomFileName();
                
                string ext = string.Empty;
                if (request.Image.ContentType == "image/jpeg") ext = ".jpeg";
                else if (request.Image.ContentType == "image/jpg") ext = ".jpg";
                else if (request.Image.ContentType == "image/png") ext = ".png";
                
                var finalImageName = Path.GetFileNameWithoutExtension(safeImageName) + ext;

                newGame.ImageName = finalImageName;

				var imagesPath = Path.Combine(webHostEnvironment.WebRootPath, "gameImages");
                var filePath = Path.Combine(imagesPath, finalImageName);
                await request.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            }

            await gameRepository.Create(newGame);
        }
    }
}
