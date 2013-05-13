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
				cfg.AddProfile(new PostProfile());
			});
		}
	}
}