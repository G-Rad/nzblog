using System;
using System.Net;
using System.Web.Mvc;
using NLog;

namespace Web.Areas.Monitor.Controllers
{
	public class ActionController : Controller
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		 public ActionResult Exclude()
		 {
			 return View();
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
				 var s = ((ActionResult)null).ToString();
			 }
			 catch (Exception ex)
			 {
				 Logger.ErrorException("This is a test error log record", ex);
			 }

			 return new HttpStatusCodeResult(HttpStatusCode.OK);
		 }
	}
}