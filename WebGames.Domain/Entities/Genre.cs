namespace WebGames.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;

        public List<Game> Games { get; } = [];
        public List<GameGenre> GameGenres { get; } = [];
    }
}
