﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Blog;

namespace Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static IContainer AutofaqContainer { get; set; }


		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			AutofacConfig.Register(this);
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			MappingConfig.Configure();
		}
	}
}