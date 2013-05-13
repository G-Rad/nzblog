using System.Web.Mvc;
using Core.Repositories;
using NLog;
using Web.Models.Widget;

namespace Web.Controllers
{
	public class WidgetController : Controller
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private readonly IInstagramRepository _instagramRepository;
		private readonly IFlickrRepository _flickrRepository;

		public WidgetController(
			IInstagramRepository instagramRepository,
			IFlickrRepository flickrRepository
			)
		{
			Logger.Trace("WidgetController created");

			_instagramRepository = instagramRepository;
			_flickrRepository = flickrRepository;
		}

		public ActionResult Index()
		{
			var instagramList = _instagramRepository.GetTop(6);
			var flickrList = _flickrRepository.GetTop(6);

			var model = new WidgetIndexModel
							{
								InstagramList = instagramList,
								FlickrList = flickrList
							};

			return View(model);
		}
	}
}