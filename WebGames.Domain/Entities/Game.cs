namespace WebGames.Domain.Entities
{
    public class Game
    {
        public required int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Source { get; set; } = default!;

        public string EncodedName { get; private set; } = default!;

        public void EncodeName()
        {
            EncodedName = Name.ToLower().Replace(" ", "-");
        }
    }
}
