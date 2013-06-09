using System;
using System.Collections.Generic;
using System.Linq;
using Core.Repositories;

namespace Blog.Core.Templating
{
	public class PhotosTemplate
	{
		public static readonly PhotosTemplate Instance = new PhotosTemplate();

		public string Render(IFlickrRepository flickrRepository, string ids)
		{
			var splittedId = ids.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
			var idList = splittedId.Select(int.Parse).ToList();

			var result = "<div class='post-imageset-gallery'>";
			foreach (var id in idList)
			{
				var photo = flickrRepository.GetById(id);
				result +=
					string.Format("<a class='fancybox' href='{0}' rel='group' title='{1}'><img alt='' src='{2}'></a>", photo.UrlImageMedium, photo.Description, photo.UrlImageSquareLarge);
			}
			result += "</div>";

			return result;
		}
	}
}