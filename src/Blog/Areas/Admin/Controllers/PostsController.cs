using System.Web.Mvc;
using Core.Repositories;

namespace Web.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class PostsController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPostRepository _postRepository;

		public PostsController(
			IUnitOfWork unitOfWork,
			IPostRepository postRepository)
		{
			_unitOfWork = unitOfWork;
			_postRepository = postRepository;
		}

		public ActionResult Index()
		{
			return View(_postRepository.GetAll());
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var post = _postRepository.GetById(id);
			return View(post);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(int id, string body)
		{
			using (var unit = _unitOfWork.BeginTransaction())
			{
				var post = _postRepository.GetById(id);
				post.Body = body;
				_postRepository.Save(post);

				unit.Commit();
			}

			return RedirectToAction("Index");
		}
	}
}