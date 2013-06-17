using System.Web.Script.Serialization;

namespace Web.Areas.Admin.Models.Flickr
{
	public class FlickrIndexModel
	{
		public FlickrModel[] Photos { get; set; }

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