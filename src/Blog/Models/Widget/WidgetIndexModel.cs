using System.Collections.Generic;
using Core.Domain;

namespace Web.Models.Widget
{
	public class WidgetIndexModel
	{
		public IList<Instagram> InstagramList { get; set; }

		public IList<Flick> FlickrList { get; set; }
	}
}