using Microsoft.AspNetCore.Identity;

namespace WebGames.Infrastructure.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string Description { get; set; } = string.Empty;
        public uint Level { get; set; } = 0;
    }
}
