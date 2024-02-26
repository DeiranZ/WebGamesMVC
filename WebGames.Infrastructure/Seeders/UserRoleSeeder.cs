using Microsoft.AspNetCore.Identity;
using WebGames.Infrastructure.Persistence;
using WebGames.Infrastructure.Users;

namespace WebGames.Infrastructure.Seeders
{
	public class UserRoleSeeder
	{
		private readonly WebGamesDbContext dbContext;

		public UserRoleSeeder(WebGamesDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task Seed()
		{
			if (await dbContext.Database.CanConnectAsync())
			{
				if (!dbContext.UserRoles.Any())
				{
					IdentityUserRole<string> identityUserRole = new IdentityUserRole<string>()
					{
						RoleId = "16251CC4-0515-4047-8FAB-AC687BAAE925".ToLower(),
						UserId = "B3DE1F1A-D1D3-4C5A-B17A-78C59B4D584A".ToLower()
					};

					dbContext.UserRoles.Add(identityUserRole);

					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
