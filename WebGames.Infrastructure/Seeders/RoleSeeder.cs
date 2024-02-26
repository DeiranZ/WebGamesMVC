using Microsoft.AspNetCore.Identity;
using WebGames.Infrastructure.Persistence;
using WebGames.Domain.ApplicationUser;

namespace WebGames.Infrastructure.Seeders
{
	public class RoleSeeder
	{
		private readonly WebGamesDbContext dbContext;

		public RoleSeeder(WebGamesDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task Seed()
		{
			if (await dbContext.Database.CanConnectAsync())
			{
				if (!dbContext.Roles.Any())
				{
					var admin = new IdentityRole()
					{
						Id = "16251CC4-0515-4047-8FAB-AC687BAAE925".ToLower(),
						Name = "Admin",
						ConcurrencyStamp = "1",
						NormalizedName = "ADMIN"
					};

					var moderator = new IdentityRole()
					{
						Id = "1FA455A3-D9A0-491D-9B32-0AB3C8090722".ToLower(),
						Name = "Moderator",
						ConcurrencyStamp = "2",
						NormalizedName = "MODERATOR"
					};

					var user = new IdentityRole()
					{
						Id = "B1ED7E6C-D3EF-45CB-A781-AC504E554528".ToLower(),
						Name = "User",
						ConcurrencyStamp = "3",
						NormalizedName = "USER"
					};

					dbContext.Roles.Add(admin);
					dbContext.Roles.Add(moderator);
					dbContext.Roles.Add(user);

					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
