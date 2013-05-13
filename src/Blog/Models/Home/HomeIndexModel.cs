using System.Collections.Generic;
using Core.Domain;

namespace Web.Models.Home
{
	public class HomeIndexModel
	{
		public IList<Core.Domain.Post> Posts { get; set; }
	}
}