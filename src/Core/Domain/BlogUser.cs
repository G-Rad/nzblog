namespace Core.Domain
{
	public class BlogUser : IEntity<int>
	{
		public virtual int Id { get; set; }

		public virtual string Username { get; set; }

		public virtual string Password { get; set; }

		public virtual bool IsAdmin { get; set; }
	}
}