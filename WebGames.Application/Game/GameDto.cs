namespace WebGames.Application.Game
{
    public class GameDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Source {  get; set; }
        public string EncodedName { get; set; } = default!;

        public bool IsEditable { get; set; }
    }
}
