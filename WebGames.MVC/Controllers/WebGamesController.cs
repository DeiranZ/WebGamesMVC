using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGames.Application.Game.Commands.CreateGame;
using WebGames.Application.Game.Commands.DeleteGame;
using WebGames.Application.Game.Commands.EditGame;
using WebGames.Application.Game.Queries.GetAllGames;
using WebGames.Application.Game.Queries.GetGameByEncodedName;
using WebGames.Application.GameGenre.Commands.AddGenreToGame;
using WebGames.Application.GameGenre.Commands.RemoveGenreFromGame;
using WebGames.Application.GameGenre.Queries.GetAllGenresOfGame;
using WebGames.Application.GameGenre.Queries.GetAllGenresOfGameExcludingExisting;
using WebGames.Application.Genre.Queries.GetAllGenres;
using WebGames.MVC.Extensions;

namespace WebGames.MVC.Controllers
{
    public class WebGamesController : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public WebGamesController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var games = await mediator.Send(new GetAllGamesQuery());
            foreach (var game in games)
            {
                var genres = await mediator.Send(new GetAllGenresOfGameQuery(game.EncodedName));
                game.Genres = genres;
            }
            return View(games);
        }

        public IActionResult NoAccess()
        {
            return View();
        }

        [Route("Home")]
        public IActionResult Logout()
        {
            return RedirectToAction(nameof(Index));
        }

        [Route("{encodedName}")]
        public async Task<IActionResult> Play(string encodedName)
        {
            var dto = await mediator.Send(new GetGameByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [Route("{encodedName}/edit")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await mediator.Send(new GetGameByEncodedNameQuery(encodedName));

            var genres = await mediator.Send(new GetAllGenresQuery());

            EditGameCommand model = mapper.Map<EditGameCommand>(dto);
            model.AllGenres = genres;

            return View(model);
        }

        [HttpPost]
        [Route("{encodedName}/edit")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Edit(EditGameCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }

        [Route("{encodedName}/delete")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Delete(string encodedName)
        {
            var dto = await mediator.Send(new GetGameByEncodedNameQuery(encodedName));

            DeleteGameCommand model = mapper.Map<DeleteGameCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("{encodedName}/delete")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Delete(DeleteGameCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Create(CreateGameCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);

            this.SetNotification("success", "Success!", $"Created game: {model.Name}");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        [Route("WebGames/AddGenreToGame/{genreEncodedName}/{gameEncodedName}")]
        public async Task<IActionResult> AddGenreToGame(string genreEncodedName, string gameEncodedName)
        {
            var model = new AddGenreToGameCommand() { GameEncodedname = gameEncodedName, GenreEncodedname = genreEncodedName };
            await mediator.Send(model);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        [Route("WebGames/RemoveGenreFromGame/{genreEncodedName}/{gameEncodedName}")]
        public async Task<IActionResult> RemoveGenreFromGame(string genreEncodedName, string gameEncodedName)
        {
            var model = new RemoveGenreFromGameCommand() { GameEncodedname = gameEncodedName, GenreEncodedname = genreEncodedName };
            await mediator.Send(model);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator")]
        [Route("WebGames/{encodedName}/GetGenres")]
        public async Task<IActionResult> GetGenresOfGame(string encodedName)
        {
            var data = await mediator.Send(new GetAllGenresOfGameQuery(encodedName));

            return Ok(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator")]
        [Route("WebGames/{encodedName}/GetAllGenresExcludingExisting")]
        public async Task<IActionResult> GetAllGenresExcludingExistingOfGame(string encodedName)
        {
            var data = await mediator.Send(new GetAllGenresOfGameExcludingExistingQuery(encodedName));

            return Ok(data);
        }
    }
}
