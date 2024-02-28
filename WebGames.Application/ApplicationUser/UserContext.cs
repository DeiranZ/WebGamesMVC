using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebGames.Domain.ApplicationUser;

namespace WebGames.Application.ApplicationUser
{
	public class UserContext : IUserContext
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<Domain.ApplicationUser.ApplicationUser> userManager;

		public UserContext(IHttpContextAccessor httpContextAccessor, UserManager<Domain.ApplicationUser.ApplicationUser> userManager)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
		}

		public CurrentUser? GetCurrentUser()
		{
			var user = httpContextAccessor.HttpContext?.User;

			if (user == null)
			{
				throw new InvalidOperationException("Context user is not present.");
			}

			if (user.Identity == null || !user.Identity.IsAuthenticated)
			{
				return null;
			}

			var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
			var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
			var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
			var level = userManager.GetUserAsync(user).Result!.Level;

			return new CurrentUser(id, email, roles, level);
		}
	}
}
