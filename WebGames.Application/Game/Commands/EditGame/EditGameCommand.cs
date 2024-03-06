using MediatR;
using Microsoft.AspNetCore.Http;
using WebGames.Application.Genre;

namespace WebGames.Application.Game.Commands.EditGame
{
    public class EditGameCommand : GameDto, IRequest
    {
        public IFormFile? Image {  get; set; }
        public IFormFile? SourceFile { get; set; }
        public IEnumerable<GenreDto> AllGenres { get; set; } = [];
    }
}
