using Core.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;

namespace Core.Repositories.Mapping
{
	public class FlickMapping : IAutoMappingOverride<Flick>
	{
		public void Override(AutoMapping<Flick> mapping)
		{
			mapping.IgnoreProperty(flick => flick.UrlImageMedium500);
			mapping.IgnoreProperty(flick => flick.UrlImageMedium800);
			mapping.IgnoreProperty(flick => flick.UrlImageSquareLarge);
			mapping.IgnoreProperty(flick => flick.UrlImageSquareSmall);
			mapping.IgnoreProperty(flick => flick.Tags);

			mapping.Map(x => x.Tags).Column("Tags").Access.CamelCaseField(Prefix.Underscore).CustomType<string>();
		}
	}
}