namespace WebGames.Application.ApplicationUser
{
	public class CurrentUser
	{
		public CurrentUser(string id, string email, IEnumerable<string> roles, uint level)
		{
			Id = id;
			Email = email;
			Roles = roles;
			Level = level;
		}

		public string Id { get; set; }
		public string Email { get; set; }
		public IEnumerable<string> Roles { get; set; }
		public uint Level { get; set; }

		public bool IsInRole(string role)
		{
			return Roles.Contains(role);
		}
	}
}
