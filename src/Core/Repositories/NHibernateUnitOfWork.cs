using System;
using NHibernate;
using NLog;

namespace Core.Repositories
{
	public class NHibernateUnitOfWork : IUnitOfWork, INHibernateUnitOfWork
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		[ThreadStatic]
		private static object _rootFlag;

		private readonly ISession _session;

		private ITransaction Transaction
		{
			get { return Session.Transaction; }
		}

		public ISession Session { get { return _session; } }

		public NHibernateUnitOfWork(ISession session)
		{
			_session = session;
			Logger.Debug("Created. Session ID: " + session.GetSessionImplementation().SessionId);
		}

		IUnitOfWork IUnitOfWork.BeginTransaction()
		{
			return (this as INHibernateUnitOfWork).BeginTransaction();
		}

		INHibernateUnitOfWork INHibernateUnitOfWork.BeginTransaction()
		{
			INHibernateUnitOfWork result = null;

			if (_rootFlag == null)
			{
				result = this;
				_rootFlag = new object();
				_isRoot = true;
				var transaction = _session.BeginTransaction();

				Logger.Debug(string.Format(
					"Transaction started. Session ID: {0} Transaction hash: {1}",
					_session.GetSessionImplementation().SessionId,
					transaction.GetHashCode()));
			}
			else
			{
				Logger.Debug(string.Format("Started as included. Session ID: {0} Transaction hash: {1}", _session.GetSessionImplementation().SessionId, Transaction.GetHashCode()));

				result = new NHibernateUnitOfWork(_session);
			}

			return result;
		}

		public void Commit()
		{
			if (_isRoot)
			{
				if (!Transaction.IsActive)
					Logger.Debug(string.Format("Commit with inactive transaction. Session ID: {0} Transaction hash: {1}", Session.GetSessionImplementation().SessionId, Transaction.GetHashCode()));
				try
				{
					Transaction.Commit();
				}
				catch
				{
					Logger.Debug(string.Format("Exception on transaction commit. Session ID: {0} Transaction hash: {1}", Session.GetSessionImplementation().SessionId, Transaction.GetHashCode()));
				}

				_rootFlag = null;

				Logger.Debug(string.Format("Commited with transaction commit. Session ID: {0} Transaction hash: {1}", Session.GetSessionImplementation().SessionId, Transaction.GetHashCode()));
			}
			else
			{
				Session.Flush();
				Logger.Debug(string.Format("Flushed. Session ID: {0} Transaction hash: {1}", Session.GetSessionImplementation().SessionId, Transaction.GetHashCode()));
			}
		}

		private bool _isRoot;

		public void Dispose()
		{
			if (_isRoot)
			{
				if (Transaction.IsActive)
				{
					Transaction.Rollback();
					Logger.Debug("Disposed with transaction Rollback. Transaction hash: " + Transaction.GetHashCode());
				}
			}
		}

	}
}