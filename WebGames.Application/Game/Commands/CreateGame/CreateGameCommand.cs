using MediatR;
using Microsoft.AspNetCore.Http;

namespace WebGames.Application.Game.Commands.CreateGame
{
    public class CreateGameCommand : GameDto, IRequest
    {
		public IFormFile? Image { get; set; }
	}
}
