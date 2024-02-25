using Microsoft.AspNetCore.Mvc;
using WebGames.Application.Game;
using WebGames.Application.Services;

namespace WebGames.MVC.Controllers
{
    public class WebGamesController : Controller
    {
        private readonly IGameService gameService;

        public WebGamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameDto game)
        {
            if (ModelState.IsValid == false) return View(game);

            await gameService.Create(game);
            return RedirectToAction(nameof(Create)); // TODO: refactor
        }
    }
}
