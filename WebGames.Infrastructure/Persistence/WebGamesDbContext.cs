using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebGames.Domain.Entities;
using WebGames.Domain.ApplicationUser;

namespace WebGames.Infrastructure.Persistence
{
    public class WebGamesDbContext : IdentityDbContext<ApplicationUser>
    {
        public WebGamesDbContext(DbContextOptions<WebGamesDbContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
