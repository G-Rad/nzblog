using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Blog
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}