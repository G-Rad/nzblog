using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using NHibernate.Linq;
using NLog;

namespace Core.Repositories
{
	public class FlickrRepository : BaseRepository<Flick, int>, IFlickrRepository
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public FlickrRepository(INHibernateUnitOfWork unitOfWorkFactory) : base(unitOfWorkFactory)
		{
			Logger.Trace("FlickrRepository created");
		}

		public IList<Flick> GetTop(int value)
		{
			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				return unit.Session.Query<Flick>().OrderByDescending(x => x.Id).Take(value).ToList();
			}
		}

		public IList<Flick> GetByTag(string tag)
		{
			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				return unit.Session.Query<Flick>()
					.ToList()
					.Where(x => x.Tags.Contains(tag))
					.OrderByDescending(x => x.Id)
					.ToList();
			}
		}
	}
}