using System.Collections.Generic;
using Core.Domain;

namespace Core.Repositories
{
	public interface IInstagramRepository : IBaseRepository<Instagram, int>
	{
		IList<Instagram> GetTop(int value);
	}
}