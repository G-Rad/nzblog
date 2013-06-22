using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Core.Services;

namespace Web.Areas.Monitor.Controllers
{
	public class InfoController : Controller
	{
		private readonly IWebsiteInfoProvider _websiteInfoProvider;
		private readonly IFlickrService _flickrService;

		public InfoController(
			IWebsiteInfoProvider websiteInfoProvider,
			IFlickrService flickrService)
		{
			_websiteInfoProvider = websiteInfoProvider;
			_flickrService = flickrService;
		}

		public ActionResult Index()
		{
			return View(_websiteInfoProvider.GetInfo());
		}

		public ActionResult Flickr()
		{
			var availablePhotos = _flickrService.GetAvailablePhotos();

			var jsonResult = new
								{
									amount = availablePhotos.Count,
									photos = availablePhotos.Select(x => new {x.PhotoId, x.Title})
								};

			return Json(jsonResult, JsonRequestBehavior.AllowGet);
		}
	}
}