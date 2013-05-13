using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Core.Services;
using NLog;

namespace Web.Filters
{
	public class InstagramGlobalFilter : ActionFilterAttribute
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Logger.Trace("OnActionExecuting has been started");

			RunInstagramService();
			RunFlickrService();
		}

		private void RunInstagramService()
		{
			Logger.Trace("RunInstagramService has been started");

			var scope = MvcApplication.AutofaqContainer.BeginLifetimeScope("thread");

			Logger.Trace("Scope created");

			var instagramService = scope.Resolve<IInstagramService>();

			Logger.Trace("InstagramService has been resolved");

			instagramService.StartUpdate(scope);
		}

		private void RunFlickrService()
		{
			Logger.Trace("RunFlickrService has been started");

			var scope = MvcApplication.AutofaqContainer.BeginLifetimeScope("thread");

			Logger.Trace("Scope created");

			var flickrService = scope.Resolve<IFlickrService>();

			Logger.Trace("FlickrService has been resolved");

			flickrService.StartUpdate(scope);
		}
	}
}