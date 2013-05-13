using NHibernate;

namespace Core.Repositories
{
	public interface INHibernateUnitOfWork : IUnitOfWork
	{
		ISession Session { get; }
		new INHibernateUnitOfWork BeginTransaction();
	}
}