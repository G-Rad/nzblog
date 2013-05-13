using System.Collections.Generic;
using Core.Domain;

namespace Core.Repositories
{
	public interface IPostRepository : IBaseRepository<Post, int>
	{
		Post[] GetByUrl(string url, bool showUnpublished = false);

		IList<Post> GetPosts(bool showUnpublished = false);
	}
}