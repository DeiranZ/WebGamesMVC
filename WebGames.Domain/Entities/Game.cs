namespace WebGames.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Source { get; set; }
        public string? ImageSource { get; set; }
        public string? ImageName {  get; set; }
        public string? SourceName { get; set; }

        public string? CreatedById { get; set; }
        public ApplicationUser.ApplicationUser? CreatedBy { get; set; }

        public string EncodedName { get; private set; } = default!;

        public List<Genre> Genres { get; } = [];
        public List<GameGenre> GameGenres { get; } = [];

        public void EncodeName()
        {
            EncodedName = Name.ToLower().Replace(" ", "-");
        }
    }
}
