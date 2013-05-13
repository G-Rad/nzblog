using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using NHibernate.Linq;
using NLog;

namespace Core.Repositories
{
	public class InstagramRepository : BaseRepository<Instagram, int>, IInstagramRepository
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public InstagramRepository(INHibernateUnitOfWork unitOfWorkFactory) : base(unitOfWorkFactory)
		{
			Logger.Trace("InstagramRepository created");
		}

		public IList<Instagram> GetTop(int value)
		{
			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				return unit.Session.Query<Instagram>().OrderByDescending(x => x.TimeCreated).Take(value).ToList();
			}
		}
	}
}