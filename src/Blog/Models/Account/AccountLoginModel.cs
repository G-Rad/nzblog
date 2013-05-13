namespace Web.Models.Account
{
	public class AccountLoginModel
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public string ReturnUrl { get; set; }
	}
}