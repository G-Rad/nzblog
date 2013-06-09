using Core.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Core.Repositories.Mapping
{
	public class PostMapping : IAutoMappingOverride<Post>
	{
		public void Override(AutoMapping<Post> mapping)
		{
			mapping.IgnoreProperty(x => x.Summary);
			mapping.IgnoreProperty(x => x.FormattedBody);

			//Setting the length to anything over 4001 will generate an NVarchar(MAX)
			//http://serialseb.blogspot.co.nz/2009/01/fluent-nhibernate-and-nvarcharmax.html
			mapping.Map(x => x.Body).Length(10000);
		}

	}
}