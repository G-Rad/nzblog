using System.Web.Mvc;

namespace Web.Areas.Monitor
{
	public class MonitorAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Monitor";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Info_default",
				"Monitor",
				new { action = "Index", Controller = "Info" }
			);

			context.MapRoute(
				"Monitor_default",
				"Monitor/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
