using System;

namespace Core.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		IUnitOfWork BeginTransaction();
	}
}