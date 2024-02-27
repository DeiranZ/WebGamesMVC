using MediatR;

namespace WebGames.Application.Genre.Queries.GetGenreByEncodedName
{
    public class GetGenreByEncodedNameQuery : IRequest<GenreDto>
    {
        public string EncodedName { get; set; }

        public GetGenreByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
