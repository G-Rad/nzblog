using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Repositories;
using Core.Services;
using NLog;
using Web.Filters;
using Web.Models.Home;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private readonly IPostRepository _postRepository;

		public HomeController(IPostRepository postRepository)
		{
			Logger.Trace("HomeController created");

			_postRepository = postRepository;
		}

		[InstagramGlobalFilter]
		public ActionResult Index(bool preview = false)
		{
			var posts = _postRepository.GetPosts(preview).OrderByDescending(x => x.DatePublished).ToList();

			var model = new HomeIndexModel
							{
								Posts = posts
							};

			return View(model);
		}
	}
}
