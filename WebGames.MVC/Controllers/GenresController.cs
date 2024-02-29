using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGames.Application.GameGenre.Queries.GetAllGamesOfGenre;
using WebGames.Application.GameGenre.Queries.GetAllGenresOfGame;
using WebGames.Application.Genre.Commands.CreateGenre;
using WebGames.Application.Genre.Commands.DeleteGenre;
using WebGames.Application.Genre.Commands.EditGenre;
using WebGames.Application.Genre.Queries.GetAllGenres;
using WebGames.Application.Genre.Queries.GetGenreByEncodedName;
using WebGames.MVC.Extensions;

namespace WebGames.MVC.Controllers
{
    public class GenresController : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GenresController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [Route("genres")]
        public async Task<IActionResult> Index()
        {
            var genres = await mediator.Send(new GetAllGenresQuery());
            return View(genres);
        }

        [Route("genres/{encodedName}")]
        public async Task<IActionResult> IndexGenre(string encodedName)
        {
            var games = await mediator.Send(new GetAllGamesOfGenreQuery(encodedName));
            
            if (games == null)
            {
                return View(null);
            }

            foreach (var game in games)
            {
                var genres = await mediator.Send(new GetAllGenresOfGameQuery(game.EncodedName));
                game.Genres = genres;
            }
            return View(games);
        }

        [Route("genres/create")]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("genres/create")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Create(CreateGenreCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);

            this.SetNotification("success", "Success!", $"Created genre: {model.Name}");

            return RedirectToAction(nameof(Index));
        }

        [Route("genres/{encodedName}/edit")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await mediator.Send(new GetGenreByEncodedNameQuery(encodedName));

            EditGenreCommand model = mapper.Map<EditGenreCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("genres/{encodedName}/edit")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Edit(EditGenreCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }

        [Route("genres/{encodedName}/delete")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Delete(string encodedName)
        {
            var dto = await mediator.Send(new GetGenreByEncodedNameQuery(encodedName));

            DeleteGenreCommand model = mapper.Map<DeleteGenreCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("genres/{encodedName}/delete")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Delete(DeleteGenreCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
