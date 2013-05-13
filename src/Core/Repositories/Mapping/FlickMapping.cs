using Core.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Core.Repositories.Mapping
{
	public class FlickMapping : IAutoMappingOverride<Flick>
	{
		public void Override(AutoMapping<Flick> mapping)
		{
			mapping.IgnoreProperty(flick => flick.UrlImageMedium);
			mapping.IgnoreProperty(flick => flick.UrlImageSquareLarge);
		}
	}
}