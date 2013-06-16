using System;
using System.Net;
using System.Web.Mvc;
using Core.Services;
using NLog;

namespace Web.Areas.Monitor.Controllers
{
	public class InfoController : Controller
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private readonly IWebsiteInfoProvider _websiteInfoProvider;

		public InfoController(IWebsiteInfoProvider websiteInfoProvider)
		{
			_websiteInfoProvider = websiteInfoProvider;
		}

		public ActionResult Index()
		{
			return View(_websiteInfoProvider.GetInfo());
		}

		public ActionResult Exception()
		{
			throw new NullReferenceException("This a test exception");
		}

		public ActionResult Log()
		{
			Logger.Debug("This is a test debug log record");
			Logger.Warn("This is a test warn log record");
			Logger.Error("This is a test error log record");

			try
			{
				var s = ((ActionResult) null).ToString();
			}
			catch (Exception ex)
			{
				Logger.ErrorException("This is a test error log record", ex);
			}

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
	}
}