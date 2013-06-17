using System.Web.Mvc;
using AutoMapper;
using Web.MappingProfiles.Admin;

namespace Web
{
	public class MappingConfig
	{
		public static void Configure()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));

				cfg.AddProfile(new PostProfile());
			});
		}
	}
}