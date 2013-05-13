using System.Collections.Generic;
using Core.Domain;

namespace Core.Repositories
{
	public interface IFlickrRepository : IBaseRepository<Flick, int>
	{
		IList<Flick> GetTop(int value);
	}
}