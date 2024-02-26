using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebGames.Infrastructure.Persistence;
using WebGames.Domain.ApplicationUser;

namespace WebGames.Infrastructure.Seeders
{
    public class UserSeeder
    {
        private readonly WebGamesDbContext dbContext;

        public UserSeeder(WebGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Users.Any())
                {
                    var userStore = new UserStore<ApplicationUser>(dbContext);
                    //var userManager = new UserManager<ApplicationUser>(userStore);

                    ApplicationUser user = new ApplicationUser()
                    {
                        Id = "B3DE1F1A-D1D3-4C5A-B17A-78C59B4D584A".ToLower(),
                        UserName = "admin@admin.com",
                        NormalizedUserName = "ADMIN@ADMIN.COM",
                        Email = "admin@admin.com",
                        NormalizedEmail = "ADMIN@ADMIN.COM",
                        LockoutEnabled = false,
                        PhoneNumber = "1234567890",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
					};

                    PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
                    user.PasswordHash = passwordHasher.HashPassword(user, "Admin#21");

                    await userStore.CreateAsync(user);
                }
            }
        }
    }
}
