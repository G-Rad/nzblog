using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using NHibernate;
using NHibernate.Linq;
using NLog;

namespace Core.Repositories
{
	public class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : IEntity<TPrimaryKey>
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		protected readonly INHibernateUnitOfWork UnitOfWorkFactory;

		public BaseRepository(INHibernateUnitOfWork unitOfWorkFactory)
		{
			UnitOfWorkFactory = unitOfWorkFactory;
		}

		public virtual TEntity GetById(TPrimaryKey id)
		{
			TEntity entity;

			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				entity = unit.Session.Get<TEntity>(id);
				unit.Commit();
			}

			return entity;
		}

		public virtual TEntity[] GetAll()
		{
			IQueryable<TEntity> entities;

			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				entities = unit.Session.Query<TEntity>();
				unit.Commit();
			}

			return entities.ToArray();
		}

		protected void DoInTransaction(Action<ISession> action)
		{
			using (var unit = UnitOfWorkFactory.BeginTransaction())
			{
				action(unit.Session);
				unit.Commit();
			}
		}

		public virtual void Save(TEntity entity)
		{
			Logger.Debug(string.Format("Saving an entity with type: {0}", typeof(TEntity)));

			DoInTransaction(s => s.Save(entity));
		}

		public virtual void Update(TEntity entity)
		{
			Logger.Debug(string.Format("Updating an entity with type: {0} and Id: {1}", typeof(TEntity), entity.Id));

			DoInTransaction(s => s.Update(entity));
		}

		public virtual void Delete(TEntity entity)
		{
			Logger.Debug(string.Format("Deleting an entity with type: {0} and Id: {1}", typeof(TEntity), entity.Id));

			DoInTransaction(s => s.Delete(entity));
		}
	}
}