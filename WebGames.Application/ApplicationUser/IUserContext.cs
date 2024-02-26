namespace WebGames.Application.ApplicationUser
{
	public interface IUserContext
	{
		CurrentUser? GetCurrentUser();
	}
}