using MediatR;

namespace WebGames.Application.Game.Queries.GetGameByEncodedName
{
    public class GetGameByEncodedNameQuery : IRequest<GameDto>
    {
        public string EncodedName { get; set; }

        public GetGameByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
