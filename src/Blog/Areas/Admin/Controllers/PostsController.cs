using System.Web.Mvc;
using AutoMapper;
using Core.Repositories;
using Web.Areas.Admin.Models;

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
			var model = Mapper.Map<PostModel>(post);
			
			return View(model);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(PostModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			using (var unit = _unitOfWork.BeginTransaction())
			{
				var post = _postRepository.GetById(model.Id);

				Mapper.Map(model, post);

				_postRepository.Save(post);

				unit.Commit();
			}

			return RedirectToAction("Index");
		}
	}
}