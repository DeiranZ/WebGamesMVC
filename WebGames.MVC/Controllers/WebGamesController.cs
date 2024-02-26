using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebGames.Application.Game;
using WebGames.Application.Game.Commands.CreateGame;
using WebGames.Application.Game.Commands.DeleteGame;
using WebGames.Application.Game.Commands.EditGame;
using WebGames.Application.Game.Queries.GetAllGames;
using WebGames.Application.Game.Queries.GetGameByEncodedName;

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
            return View(games);
        }

        [Route("{encodedName}")]
        public async Task<IActionResult> Play(string encodedName)
        {
            var dto = await mediator.Send(new GetGameByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [Route("{encodedName}/edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await mediator.Send(new GetGameByEncodedNameQuery(encodedName));

            EditGameCommand model = mapper.Map<EditGameCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("{encodedName}/edit")]
        public async Task<IActionResult> Edit(EditGameCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }

        [Route("{encodedName}/delete")]
        public async Task<IActionResult> Delete(string encodedName)
        {
            var dto = await mediator.Send(new GetGameByEncodedNameQuery(encodedName));

            DeleteGameCommand model = mapper.Map<DeleteGameCommand>(dto);
            return View(model);
        }
        
        [HttpPost]
        [Route("{encodedName}/delete")]
        public async Task<IActionResult> Delete(DeleteGameCommand model)
        {
            if (ModelState.IsValid == false) return View(model);

            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
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
