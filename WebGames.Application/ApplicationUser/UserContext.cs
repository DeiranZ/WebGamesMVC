using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WebGames.Application.ApplicationUser
{
	public class UserContext : IUserContext
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		public UserContext(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		public CurrentUser GetCurrentUser()
		{
			var user = httpContextAccessor.HttpContext?.User;

			if (user == null)
			{
				throw new InvalidOperationException("Context user is not present.");
			}

			var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
			var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;

			return new CurrentUser(id, email);
		}
	}
}
