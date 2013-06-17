using System;
using System.Net;
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
		public ActionResult Add()
		{
			var defaultModel = new PostModel
				{
					DateCreated = DateTime.Now, 
					DatePublished = DateTime.Now
				};

			return View(defaultModel);
		}

		[HttpPost]
		public ActionResult Add(PostModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var post = model.ToPost();

			_postRepository.Save(post);

			return RedirectToAction("Edit", new {id = post.Id});
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var post = _postRepository.GetById(id);
			var model = Mapper.Map<EditPostModel>(post);
			
			return View(model);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(EditPostModel model)
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

		[HttpPost]
		public ActionResult Delete(int id)
		{
			ActionResult result;

			using (var unit = _unitOfWork.BeginTransaction())
			{
				var post = _postRepository.GetById(id);

				if (post != null)
				{
					_postRepository.Delete(post);
					result = new HttpStatusCodeResult(HttpStatusCode.OK);
				}
				else
				{
					result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Post with given id doesn't exist");
				}

				unit.Commit();
			}

			return result;
		}
	}
}