using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebGames.Application.Game;
using WebGames.Application.Game.Commands.CreateGame;
using WebGames.Application.Game.Queries.GetAllGames;

namespace WebGames.MVC.Controllers
{
    public class WebGamesController : Controller
    {
        private readonly IMediator mediator;

        public WebGamesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var games = await mediator.Send(new GetAllGamesQuery());
            return View(games);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
