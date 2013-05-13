using System.Linq;
using Core.Domain;
using NHibernate.Linq;

namespace Core.Repositories
{
	public interface IUserRepository : IBaseRepository<BlogUser, int>
	{
		BlogUser GetUser(string username);
	}

	public class UserRepository : BaseRepository<BlogUser, int>, IUserRepository
	{
		public UserRepository(INHibernateUnitOfWork unitOfWorkFactory) : base(unitOfWorkFactory)
		{
		}

		public BlogUser GetUser(string username)
		{
			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				return unit.Session.Query<BlogUser>().FirstOrDefault(x => x.Username == username);
			}
		}
	}
}