using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Domain;
using NHibernate.Linq;
using NLog;

namespace Core.Repositories
{
	public class PostRepository : BaseRepository<Post, int>, IPostRepository
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		Expression<Func<Post, bool>> _publishedFilter = p => p.IsPublished;

		public PostRepository(INHibernateUnitOfWork unitOfWorkFactory) : base(unitOfWorkFactory)
		{
			Logger.Trace("PostRepository created");
		}

		public Post[] GetByUrl(string url, bool showUnpublished = false)
		{
			Post post;
			Post next = null;
			Post prev = null;

			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				if (showUnpublished)
					_publishedFilter = p => true;

				post = unit.Session.Query<Post>().Where(_publishedFilter).FirstOrDefault(x => x.Url.ToLower() == url.ToLower());

				if (post != null)
				{
					next = unit.Session.Query<Post>().OrderBy(x => x.DatePublished).FirstOrDefault(x => x.IsPublished && x.DatePublished > post.DatePublished);
					prev = unit.Session.Query<Post>().OrderByDescending(x => x.DatePublished).FirstOrDefault(x => x.IsPublished && x.DatePublished < post.DatePublished);
				}

				unit.Commit();
			}

			return new [] { post , next, prev };
		}

		public IList<Post> GetPosts(bool showUnpublished = false)
		{
			if (showUnpublished)
				_publishedFilter = p => true;

			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				return unit.Session.Query<Post>().Where(_publishedFilter).OrderByDescending(x => x.DatePublished).ToList();
			}
		}
	}
}