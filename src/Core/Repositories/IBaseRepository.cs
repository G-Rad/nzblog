using Core.Domain;

namespace Core.Repositories
{
	public interface IBaseRepository<TEntity, in TPrimaryKey> where TEntity : IEntity<TPrimaryKey>
	{
		TEntity GetById(TPrimaryKey id);
		TEntity[] GetAll();
		void Save(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
	}
}