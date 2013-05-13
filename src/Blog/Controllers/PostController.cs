using System.Net;
using System.Web.Mvc;
using Core.Repositories;
using Web.Models.Post;

namespace Web.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostRepository _postRepository;

		public PostController(IPostRepository postRepository)
		{
			_postRepository = postRepository;
		}

		public ActionResult Post(string url, bool preview = false)
		{
			var posts = _postRepository.GetByUrl(url, preview);

			if (posts[0] == null)
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);

			var model = new PostPostModel
							{
								Post = posts[0],
								Next = posts[1],
								Prev = posts[2]
							};

			return View(model);
		}
	}
}