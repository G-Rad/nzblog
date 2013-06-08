using System.Web.Script.Serialization;
using Core.Domain;

namespace Web.Areas.Admin.Models.Flickr
{
	public class FlickrIndexModel
	{
		public Flick[] Photos { get; set; }

		public string PhotosJson
		{
			get
			{
				var serializer = new JavaScriptSerializer();
				return serializer.Serialize(Photos);
			}
		}

	}
}