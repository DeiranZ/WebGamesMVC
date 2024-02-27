using AutoMapper;
using MediatR;
using WebGames.Application.ApplicationUser;
using WebGames.Domain.Entities;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Genre.Commands.CreateGenre
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;
        private readonly IUserContext userContext;

        public CreateGenreCommandHandler(IGenreRepository genreRepository, IMapper mapper, IUserContext userContext)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
            this.userContext = userContext;
        }

        public async Task Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            if (currentUser == null || !(currentUser.IsInRole("Admin") && currentUser.IsInRole("Moderator")))
            {
                return;
            }

            var newGenre = mapper.Map<Domain.Entities.Genre>(request);

            newGenre.CreatedById = currentUser.Id;

            newGenre.EncodeName();
            await genreRepository.Create(newGenre);
        }
    }
}
