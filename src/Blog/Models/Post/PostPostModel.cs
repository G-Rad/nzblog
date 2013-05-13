namespace Web.Models.Post
{
	public class PostPostModel
	{
		public Core.Domain.Post Post { get; set; }

		public Core.Domain.Post Next { get; set; }

		public Core.Domain.Post Prev { get; set; }
	}
}