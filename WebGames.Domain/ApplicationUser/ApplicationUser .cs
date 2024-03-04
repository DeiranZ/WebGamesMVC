using Microsoft.AspNetCore.Identity;

namespace WebGames.Domain.ApplicationUser
{
    public class ApplicationUser : IdentityUser
    {
        public string Description { get; set; } = string.Empty;
        public uint Level { get; set; } = 1;
    }
}
