namespace WebGames.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? CreatedById { get; set; }
        public ApplicationUser.ApplicationUser? CreatedBy { get; set; }
        
        public string EncodedName { get; private set; } = default!;
        public void EncodeName()
        {
            EncodedName = Name.ToLower().Replace(" ", "-");
        }

        public List<Game> Games { get; } = [];
        public List<GameGenre> GameGenres { get; } = [];
    }
}
